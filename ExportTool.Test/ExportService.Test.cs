using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;
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
        var exportService =
            new ExportService<Client>(_clientService, "C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.csv");
        var clients = _clientService.GetEntities(1, 10);

        // Act
        exportService.Export(clients.ToList());

        // Assert
        var filePath = "C:\\Users\\Hi-Tech\\OneDrive\\Desktop\\clients.csv";
        var lines = File.ReadAllLines(filePath);
        Assert.NotEmpty(lines);
    }

    [Fact]
    public void ImportClients_ShouldImportFromCSV()
    {
        // Arrange
        var exportService =
            new ExportService<Client>(_clientService, "C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.csv");
        var clients = _dataGenerator.GenerateClients(10);
        exportService.Export(clients);
        var expectedClientName = clients[0].Name;

        // Act
        var filePath = "C:\\Users\\Hi-Tech\\OneDrive\\Desktop\\clients.csv";
        exportService.Import(filePath);

        // Assert
        var importedClients = _clientService.GetEntities(1, 10).ToList();
        Assert.Contains(importedClients, c => c.Name == expectedClientName);
    }

    [Fact]
    public void ExportClientsToJson_ShouldExportToJsonFile()
    {
        // Arrange
        var exportService =
            new ExportService<Client>(_clientService, "C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.json");
        var clients = _dataGenerator.GenerateClients(10);
        var filePath = "C:\\Users\\Hi-Tech\\OneDrive\\Desktop\\clients.json";

        // Act
        exportService.ExportEntitiesToJson(clients, filePath);

        // Assert
        Assert.True(File.Exists(filePath));
        var fileContent = File.ReadAllText(filePath);
        Assert.Contains(clients[0].Name, fileContent);
    }

    [Fact]
    public void ImportClientsFromJson_ShouldImportFromJsonFile()
    {
        // Arrange
        var exportService =
            new ExportService<Client>(_clientService, "C:\\Users\\Hi-Tech\\OneDrive\\Desktop", "clients.json");
        var clients = _dataGenerator.GenerateClients(10);
        var filePath = "C:\\Users\\Hi-Tech\\OneDrive\\Desktop\\clients.json";
        exportService.ExportEntitiesToJson(clients, filePath);
        var expectedClientName = clients[0].Name;

        // Act
        var importedClients = exportService.ImportEntitiesFromJson(filePath).ToList();

        // Assert
        Assert.NotNull(importedClients);
        Assert.Contains(importedClients, c => c.Name == expectedClientName);
    }

    [Fact]
    public void ExportSingleClientToJson_ShouldExportSingleClientToJsonFile()
    {
        // Arrange
        var exportService = new ExportService<Client>(_clientService, "C:\\Users\\Hi-Tech\\OneDrive\\Desktop",
            "single_client.json");
        var client = _dataGenerator.GenerateClients(1).First();
        var filePath = "C:\\Users\\Hi-Tech\\OneDrive\\Desktop\\client.json";

        // Act
        exportService.ExportEntityToJson(client, filePath);

        // Assert
        Assert.True(File.Exists(filePath));
        var fileContent = File.ReadAllText(filePath);
        Assert.Contains(client.Name, fileContent);
    }

    [Fact]
    public void ImportSingleClientFromJson_ShouldImportSingleClientFromJsonFile()
    {
        // Arrange
        var exportService = new ExportService<Client>(_clientService, "C:\\Users\\Hi-Tech\\OneDrive\\Desktop",
            "single_client.json");
        var client = _dataGenerator.GenerateClients(1).First();
        var filePath = "C:\\Users\\Hi-Tech\\OneDrive\\Desktop\\client.json";
        exportService.ExportEntityToJson(client, filePath);
        var expectedClientName = client.Name;

        // Act
        var importedClient = exportService.ImportEntityFromJson(filePath);

        // Assert
        Assert.NotNull(importedClient);
        Assert.Equal(expectedClientName, importedClient.Name);
    }
}