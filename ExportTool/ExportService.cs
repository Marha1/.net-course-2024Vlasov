using System.Globalization;
using BankSystem.App.Services.Interfaces;
using CsvHelper;

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
}