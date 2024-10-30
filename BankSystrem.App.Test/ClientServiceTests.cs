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
    public async Task AddClient_Success_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = "123456789";

        // Act
        await _clientService.AddAsync(client);

        // Assert
        var addedClient = await _clientService.GetByIdAsync(client.Id);
        Assert.NotNull(addedClient);
        Assert.Equal(client.Name, addedClient.Name);
    }

    [Fact]
    public async Task DeleteClient_Success_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = "123456789";
        await _clientService.AddAsync(client);

        // Act
        var result = await _clientService.DeleteAsync(client);

        // Assert
        Assert.True(result);
        var deletedClient = await _clientService.GetByIdAsync(client.Id);
        Assert.Null(deletedClient);
    }

    [Fact]
    public async Task UpdateClient_Success_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = "123456789";
        await _clientService.AddAsync(client);

        client.Name = "Updated Name";

        // Act
        var result = await _clientService.UpdateAsync(client);

        // Assert
        Assert.True(result);
        var updatedClient = await _clientService.GetByIdAsync(client.Id);
        Assert.Equal("Updated Name", updatedClient.Name);
    }

    [Fact]
    public async Task AddClient_ThrowsExceptionIfUnder18_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AgeException>(() => _clientService.AddAsync(client));
        Assert.Equal("Моложе 18 лет!", exception.Message);
    }


    [Fact]
    public async Task AddClient_ThrowsExceptionIfNoPassport_Test()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = null;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<PassportException>(() => _clientService.AddAsync(client));
        Assert.Equal("Паспортные данные отсутствуют.", exception.Message);
    }

    [Fact]
    public async Task AddAccount_ShouldAddAccount_WhenInputIsValid()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        await _clientService.AddAsync(client);

        var currencyRUB = await _context.Currencies.FirstOrDefaultAsync(c => c.Name == "RUB");
        if (currencyRUB == null)
        {
            currencyRUB = new Currency { Name = "RUB" };
            _context.Currencies.Add(currencyRUB);
            await _context.SaveChangesAsync();
        }

        var account = new Account { CurrencyId = currencyRUB.Id, Amount = 1000 };

        // Act
        await _clientService.AddAccountAsync(client.Id, account);

        // Assert
        var addedClient = await _context.Clients.Include(c => c.Accounts).FirstAsync(c => c.Id == client.Id);
        Assert.NotNull(addedClient.Accounts);
    }
    

    [Fact]
    public async Task DeleteAccount_ShouldDeleteAccount_WhenAccountExists()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        await _clientService.AddAsync(client);

        var currencyRUB = await _context.Currencies.FirstOrDefaultAsync(c => c.Name == "RUB");
        if (currencyRUB == null)
        {
            currencyRUB = new Currency { Name = "RUB" };
            _context.Currencies.Add(currencyRUB);
            await _context.SaveChangesAsync();
        }

        var account = new Account { CurrencyId = currencyRUB.Id, Amount = 1000 };
        await _clientService.AddAccountAsync(client.Id, account);

        // Act
        var result = await _clientService.DeleteAccountAsync(client.Id, currencyRUB.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetAccountsByClient_ShouldReturnAccounts_WhenClientExists()
    {
        // Arrange
        var clientId = new Guid("01928fd6-01b8-747e-9838-a67dfd8facf4");
        var client = await _clientService.GetByIdAsync(clientId);

        // Act
        var accounts = await _clientService.GetAccountsByClientAsync(client);

        // Assert
        Assert.NotNull(accounts);
    }
}