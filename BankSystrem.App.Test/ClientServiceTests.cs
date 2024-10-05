using BankSystem.App.Exceptions; 
using BankSystem.App.Services;
using BankSystem.App.Services.Storage;
using Xunit;

namespace BankSystem.App.Test;

public class ClientServiceTests
{
    private readonly TestDataGenerator _dataGenerator;

    public ClientServiceTests()
    {
        _dataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void AddClient_ThrowsExceptionIfUnder18_Test()
    {
        // Arrange
        var storage = new ClientStorage();
        var client = _dataGenerator.GenerateClients(1).First();
        client.Age = 15;

        // Act & Assert
        var exception = Assert.Throws<AgeException>(() => storage.AddClient(client));
        Assert.Equal("Моложе 18 лет!", exception.Message);
    }

    [Fact]
    public void AddClient_ThrowsExceptionIfNoPassport_Test()
    {
        // Arrange
        var storage = new ClientStorage();
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = null;

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => storage.AddClient(client));
        Assert.Equal("Паспортные данные отсутствуют.", exception.Message);
    }

    [Fact]
    public void AddClient_CreatesDefaultUSDAccount_Test()
    {
        // Arrange
        var storage = new ClientStorage();
        var client = _dataGenerator.GenerateClients(1).First();

        // Act
        storage.AddClient(client);
        var accounts = storage.GetClients().First().Value;

        // Assert
        var defaultAccount = accounts.FirstOrDefault(a => a.Currency.Name == "USD");
        Assert.NotNull(defaultAccount); 
    }

    [Fact]
    public void GetClientByName_Test()
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

}
