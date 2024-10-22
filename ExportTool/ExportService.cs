using System.Globalization;
using BankSystem.App.Services.Interfaces;
using CsvHelper;
using Newtonsoft.Json; 

public class ExportService<T>
{
    private readonly IBaseService<T> _storageService;
    private string _pathToDirectory { get; set; }
    private string _csvFileName { get; set; }

    public ExportService(IBaseService<T> storageService, string pathToDirectory, string csvFileName)
    {
        _storageService = storageService;
        _pathToDirectory = pathToDirectory;
        _csvFileName = csvFileName;
    }

    public void Export(List<T> entities)
    {
        Directory.CreateDirectory(_pathToDirectory);

        string fullPath = Path.Combine(_pathToDirectory, _csvFileName);

        using (var streamWriter = new StreamWriter(fullPath))
        using (var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            writer.WriteHeader<T>();
            writer.NextRecord();

            foreach (var entity in entities)
            {
                writer.WriteRecord(entity);
                writer.NextRecord();
            }
        }
    }

    public void Import(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("CSV файл не найден.");
        }

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var entities = csv.GetRecords<T>().ToList();
            foreach (var entity in entities)
            {
                _storageService.Add(entity);
            }
        }
    }
    
    public void ExportEntitiesToJson(IEnumerable<T> entities, string filePath)
    {
        if (entities == null)
            throw new Exception("The collection cannot be null.");

        var json = JsonConvert.SerializeObject(entities, Formatting.Indented);

        using (var streamWriter = new StreamWriter(filePath))
        {
            streamWriter.Write(json);
        }
    }

    public ICollection<T> ImportEntitiesFromJson(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file was not found.");

        using (var streamReader = new StreamReader(filePath))
        {
            var json = streamReader.ReadToEnd();
            var entities = JsonConvert.DeserializeObject<ICollection<T>>(json);

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    _storageService.Add(entity);  
                }
                return entities;
            }
            else
            {
                throw new Exception("Failed to deserialize JSON.");
            }
        }
    }

    public void ExportEntityToJson(T entity, string filePath)
    {
        if (entity == null)
            throw new Exception("The entity cannot be null.");

        var json = JsonConvert.SerializeObject(entity, Formatting.Indented);

        using (var streamWriter = new StreamWriter(filePath))
        {
            streamWriter.Write(json);
        }
    }

    public T ImportEntityFromJson(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file was not found.");

        using (var streamReader = new StreamReader(filePath))
        {
            var json = streamReader.ReadToEnd();
            var entity = JsonConvert.DeserializeObject<T>(json);

            if (entity != null)
            {
                _storageService.Add(entity);  
                return entity;
            }
            else
            {
                throw new Exception("Failed to deserialize JSON.");
            }
        }
    }
}
