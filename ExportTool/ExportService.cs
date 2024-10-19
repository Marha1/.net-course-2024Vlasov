using System.Globalization;
using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;
using CsvHelper;

namespace ExportTool;

public class ExportService
{
    private readonly ClientService _clientService;
    private readonly BankSystemDbContext _context;
    private readonly ClientStorage _storage;
    private string _pathToDirecory { get; set; }
    private string _csvFileName { get; set; }
    public ExportService(string pathToDirectory, string csvFileName)
    {
        _pathToDirecory =  pathToDirectory;
        _csvFileName =  csvFileName;
        _context = new BankSystemDbContext(); 
        _storage = new ClientStorage(_context);
        _clientService = new ClientService(_storage);
    }

    
    public void Export(List<Client> clients)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirecory);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        string fullPath = Path.Combine(_pathToDirecory, _csvFileName);
        
        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                using (var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                   
                    writer.WriteHeader<Client>();
                    writer.NextRecord();

                    foreach (var client in clients)
                    {
                        writer.WriteRecord(client);
                        writer.NextRecord();
                    }
                }
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
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                var clients = csv.GetRecords<Client>().ToList();

                foreach (var client in clients)
                {
                    _clientService.Add(client);  
                }
            }
        }
    }
}