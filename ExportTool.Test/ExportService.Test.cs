using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using Xunit;

namespace ExportTool.Test;

public class ExportService_Test
{
    private readonly ClientService _clientService;

    private readonly BankSystemDbContext _context;
    private readonly TestDataGenerator _dataGenerator;
    private readonly ClientStorage _storage;


    public ExportService_Test()
    {
        _context = new BankSystemDbContext();
        _storage = new ClientStorage(_context);
        _clientService = new ClientService(_storage);
        _dataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void ExportClients_ShouldExportToCSV()
    {
        // Arrange
        var exportService = new ExportService("C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.csv");
        var clients = _clientService.GetEntities(1, 10);

        // Act
        exportService.Export(clients.ToList());

        // Assert
        var filePath = Path.Combine("C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.csv");
        var lines = File.ReadAllLines(filePath);
        Assert.NotEmpty(lines);
    }

    [Fact]
    public void ImportClients_ShouldImportFromCSV()
    {
        // Arrange
        var exportService = new ExportService("C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.csv");
        var clients = _dataGenerator.GenerateClients(10);
        exportService.Export(clients.ToList());
        var exclient= clients[0].Name;
        // Act
        var filePath = Path.Combine("C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.csv");
        exportService.Import(filePath); 

        // Assert
        var importedClients = _clientService.GetEntities(1, 10).ToList(); 
        Assert.Contains(importedClients,c=>c.Name== exclient); 
    }
}