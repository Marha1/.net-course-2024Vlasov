using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;
using Xunit;

namespace BankSystem.App.Test;

public class ClientServiceTests
{
    private readonly ClientService _clientService;
    private readonly TestDataGenerator _dataGenerator;
    private readonly ClientStorage _storage;

    public ClientServiceTests()
    {
        _dataGenerator = new TestDataGenerator();
        _storage = new ClientStorage();
        _clientService = new ClientService(_storage);
    }

    [Fact]
    public void AddClient_ThrowsExceptionIfUnder18_Test()
    {
        // Arrange
        var storage = new ClientStorage();
        var client = _dataGenerator.GenerateClients(1).First();
        client.Age = 15;

        // Act 
        var exception = Assert.Throws<AgeException>(() => _storage.Add(client));
        //Assert
        Assert.Equal("Моложе 18 лет!", exception.Message);
    }

    [Fact]
    public void AddClient_ThrowsExceptionIfNoPassport_Test()
    {
        // Arrange
        var storage = new ClientStorage();
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = null;

        // Act 
        var exception = Assert.Throws<PassportException>(() => _storage.Add(client));
        //Assert
        Assert.Equal("Паспортные данные отсутствуют.", exception.Message);
    }

    [Fact]
    public void AddAccountToClient_Success_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.Age = 25;
        client.PhoneNumber = "dsaassa";
        _clientService.Add(client);

        var newAccount = new Account
        {
            Currency = new Currency { Name = "USD" },
            Amount = 1000
        };

        // Act
        _clientService.AddAccountToClient(client.PhoneNumber, newAccount);

        // Assert
        var accounts = _clientService.GetAccountsByPhoneNumber(client.PhoneNumber);
        var defaultAccount = accounts.FirstOrDefault(a => a.Currency.Name == "USD");
        Assert.NotNull(defaultAccount);
    }

    [Fact]
    public void GetClientByName_Test()
    {
        // Arrange
        var client1 = _dataGenerator.GenerateClients(1).First();
        client1.Age = 25;
        var client2 = _dataGenerator.GenerateClients(1).First();
        client2.Age = 30;
        _storage.Add(client1);
        _storage.Add(client2);

        // Act
        var result = _clientService.GetClientsByFilter(client1.Name,null,null,null,null,1,5);

        // Assert
        Assert.NotNull(result);
    }
}