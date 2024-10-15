using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BankSystem.App.Test;

public class ClientServiceTests
{
    private readonly ClientService _clientService;
    private readonly BankSystemDbContext _context;
    private readonly TestDataGenerator _dataGenerator;
    private readonly ClientStorage _storage;

    public ClientServiceTests()
    {
        _context = new BankSystemDbContext(); 
        _storage = new ClientStorage(_context);
        _clientService = new ClientService(_storage);
        _dataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void AddClient_Success_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = "123456789"; 
        // Act
        _clientService.Add(client);

        // Assert
        var addedClient = _clientService.GetById(client.Id);
        Assert.NotNull(addedClient);
        Assert.Equal(client.Name, addedClient.Name);
    }

    [Fact]
    public void DeleteClient_Success_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = "123456789";
        _clientService.Add(client);

        // Act
        var result = _clientService.Delete(client);

        // Assert
        Assert.True(result);
        var deletedClient = _clientService.GetById(client.Id);
        Assert.Null(deletedClient); 
    }

    [Fact]
    public void UpdateClient_Success_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = "123456789";
        _clientService.Add(client);

        client.Name = "Updated Name";

        // Act
        var result = _clientService.Update(client);

        // Assert
        Assert.True(result);
        var updatedClient = _clientService.GetById(client.Id);
        Assert.Equal("Updated Name", updatedClient.Name);
    }

    [Fact]
    public void AddClient_ThrowsExceptionIfUnder18_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();

        // Act & Assert
        var exception = Assert.Throws<AgeException>(() => _clientService.Add(client));
        Assert.Equal("Моложе 18 лет!", exception.Message);
    }

    [Fact]
    public void AddClient_ThrowsExceptionIfNoPassport_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = null;

        // Act & Assert
        var exception = Assert.Throws<PassportException>(() => _clientService.Add(client));
        Assert.Equal("Паспортные данные отсутствуют.", exception.Message);
    }
    [Fact]
    public void AddAccount_ShouldAddAccount_WhenInputIsValid()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        _clientService.Add(client);

        var currencyRUB = _context.Currencies.FirstOrDefault(c => c.Name == "RUB");
        if (currencyRUB == null)
        {
            currencyRUB = new Currency { Name = "RUB" };
            _context.Currencies.Add(currencyRUB);
            _context.SaveChanges();
        }

        var account = new Account { CurrencyId = currencyRUB.Id, Amount = 1000 };

        // Act
        _clientService.AddAccount(client.Id, account);

        // Assert
        var addedClient = _context.Clients.Include(c => c.Accounts).First(c => c.Id == client.Id);
        Assert.NotNull(addedClient.Accounts);
    }

    

    [Fact]
    public void UpdateAccount_ShouldUpdateAccount_WhenInputIsValid()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        _clientService.Add(client);

        var currencyRUB = _context.Currencies.FirstOrDefault(c => c.Name == "RUB");
        if (currencyRUB == null)
        {
            currencyRUB = new Currency { Name = "RUB" };
            _context.Currencies.Add(currencyRUB);
            _context.SaveChanges();
        }

        var account = new Account { CurrencyId = currencyRUB.Id, Amount = 1000 };
        _clientService.AddAccount(client.Id, account);

        var updatedAccount = new Account { CurrencyId = currencyRUB.Id, Amount = 1500 };

        // Act
        var result = _clientService.UpdateAccount(client.Id, updatedAccount);

        // Assert
        Assert.True(result);
        var addedClient = _context.Clients.Include(c => c.Accounts).First(c => c.Id == client.Id);
        Assert.Equal(1500, addedClient.Accounts.First(a => a.CurrencyId == currencyRUB.Id).Amount);
    }

    [Fact]
    public void DeleteAccount_ShouldDeleteAccount_WhenAccountExists()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        _clientService.Add(client);

        var currencyRUB = _context.Currencies.FirstOrDefault(c => c.Name == "RUB");
        if (currencyRUB == null)
        {
            currencyRUB = new Currency { Name = "RUB" };
            _context.Currencies.Add(currencyRUB);
            _context.SaveChanges();
        }

        var account = new Account { CurrencyId = currencyRUB.Id, Amount = 1000 };
        _clientService.AddAccount(client.Id, account);

        // Act
        var result = _clientService.DeleteAccount(client.Id, currencyRUB.Id);

        // Assert
        Assert.True(result);
        
    }

    [Fact]
    public void GetAccountsByClient_ShouldReturnAccounts_WhenClientExists()
    {
        // Arrange
        var clientId = new Guid("01928fd6-01b8-747e-9838-a67dfd8facf4");
        var client = _clientService.GetById(clientId);
        
        // Act
        var accounts = _clientService.GetAccountsByClient(client);

        // Assert
        Assert.NotNull( accounts);
    }
}