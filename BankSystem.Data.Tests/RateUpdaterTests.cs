using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;
using Xunit;

public class RateUpdaterTests
{
    private readonly ClientService _clientService;
    private readonly BankSystemDbContext _context;
    private readonly ClientStorage _storage;
    private readonly RateUpdater _rateUpdater;
    private readonly TestDataGenerator _dataGenerator;


    public RateUpdaterTests()
    {
        _context = new BankSystemDbContext(); 
        _storage = new ClientStorage(_context);
        _clientService = new ClientService(_storage);
        _rateUpdater = new RateUpdater(_storage);
        _dataGenerator = new TestDataGenerator();
    }

    [Fact]
    public async Task ApplyMonthlyRate_UpdatesAccounts_WhenOlderThan30Days()
    {
        // Arrange
        var client = _dataGenerator.GenerateClients(1).First();
        client.PassportDetails = "123456789";

        _context.Clients.Add(client);
        await _context.SaveChangesAsync(); 

        var account1 = new Account
        {
            Amount = 100m,
            LastUpdatedDate = DateTime.UtcNow.AddDays(-31),
            ClientId = client.Id ,
            CurrencyId = new Guid("01929022-88fa-7ae9-b5dd-90de5d304ccc")

        };

        var account2 = new Account
        {
            Id = Guid.NewGuid(),
            Amount = 200m,
            LastUpdatedDate = DateTime.UtcNow.AddDays(-29),
            ClientId = client.Id,
            CurrencyId = new Guid("01928fbe-8bf1-7454-8683-7141820b4649")
        };

        _context.Accounts.Add(account1);
        _context.Accounts.Add(account2);
        await _context.SaveChangesAsync(); 

        decimal interestRate = 0.05m; 
        var cancellationToken = CancellationToken.None;

        // Act
        await _rateUpdater.ApplyMonthlyRateAsync(interestRate, cancellationToken);

        var updatedAccounts = await _storage.GetAllAccount(cancellationToken);

        // Assert
        Assert.Equal(105m, updatedAccounts.First(a => a.Id == account1.Id).Amount);
    }
}