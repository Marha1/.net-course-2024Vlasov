using BankSystem.App.Services;
using BankSystem.App.Services.Storage;
using BankSystemDomain.Models;
using Xunit;

namespace BankSystrem.App.Test;

public class ClientServiceTests
{
    private readonly TestDataGenerator _dataGenerator;

    public ClientServiceTests()
    {
        _dataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void AddClient()
    {
        // Arrange
        var storage = new ClientStorage();
        var client = _dataGenerator.GenerateClients(1).First();

        // Act
        storage.AddClient(client);
        var clients = storage.GetClients();

        // Assert
        Assert.Contains(client, clients);
    }

    [Fact]
    public void GetClientsByFilter()
    {
        // Arrange
        var storage = new ClientStorage();
        var client1 = _dataGenerator.GenerateClients(1).First();
        var client2 = _dataGenerator.GenerateClients(1).First();
        storage.AddClient(client1);
        storage.AddClient(client2);
        var service = new ClientService(storage);

        // Act
        var result = service.GetClientsByFilter(client1.Name, null, null, null, null);

        // Assert
        Assert.Contains(client1, result);
        Assert.DoesNotContain(client2, result);
    }

    [Fact]
    public void GetYoungestClient()
    {
        // Arrange
        var storage = new ClientStorage();
        var client1 = _dataGenerator.GenerateClients(1).First();
        var client2 = _dataGenerator.GenerateClients(1).First();
        storage.AddClient(client1);
        storage.AddClient(client2);
        var service = new ClientService(storage);

        // Act
        var youngestClient = storage.GetYoungestClient();

        // Assert
        Assert.NotNull(youngestClient);
    }

    [Fact]
    public void GetOldestClient()
    {
        // Arrange
        var storage = new ClientStorage();
        var client1 = new Client()
        {
            Name = "Кайла",
            BirthDate = new DateTime(1990, 1, 1) 
        };

        var client2 = new Client()
        {
            Name = "Циля",
            BirthDate = new DateTime(2000, 1, 1) 
        };
        storage.AddClient(client1);
        storage.AddClient(client2);
        var service = new ClientService(storage);

        // Act
        var oldestClient = storage.GetOldestClient();

        // Assert
        Assert.Equal(client1, oldestClient);
    }

    [Fact]
    public void CalculateAverageAge()
    {
        // Arrange
        var storage = new ClientStorage();
        var client1 = _dataGenerator.GenerateClients(1).First();
        var client2 = _dataGenerator.GenerateClients(1).First();
        storage.AddClient(client1);
        storage.AddClient(client2);
        var service = new ClientService(storage);

        // Act
        var averageAge = storage.CalculateAverageAge();

        // Assert
        Assert.Equal((client1.Age + client2.Age) / 2, averageAge);
    }
}