using BankSystem.App.Services;
using BankSystemDomain.Models;
using Xunit;

namespace BankSystrem.App.Test;

public class EquivalenceTests
{
    [Fact]
    public void GetHashCodeNecessityPositiveTest()
    {
        // Arrange 
        var dataGenerator = new TestDataGenerator();
        var clients = dataGenerator.GenerateClients();
        var accounts = dataGenerator.GenerateClientAccounts(clients);
        var existingClient = clients[0];

       

        var newClientWithOverride = new Client
        {
            Name = existingClient.Name,
            Surname = existingClient.Surname,
            Age = existingClient.Age,
            PhoneNumber = existingClient.PhoneNumber
        };
        
        var containsNewClientAfterOverride = accounts.ContainsKey(newClientWithOverride);

        // Assert 
        Assert.True(containsNewClientAfterOverride);
    }

    [Fact]
    public void TestMultipleAccountsForClient()
    {
        // Arrange 
        var dataGenerator = new TestDataGenerator();
        var clients = dataGenerator.GenerateClients(10);
        var accounts = dataGenerator.GenerateClientAccounts(clients);
        var existingClient = clients[0];
        var newClientWithOverride = new Client
        {
            Name = existingClient.Name,
            Surname = existingClient.Surname,
            Age = existingClient.Age,
            PhoneNumber = existingClient.PhoneNumber
        };

        // Act 
        var accountsForExistingClient = accounts[existingClient];
        var accountsForNewClient = accounts.ContainsKey(newClientWithOverride) ? accounts[newClientWithOverride] : null;

        // Assert 
        Assert.NotNull(accountsForNewClient);
        Assert.Equal(accountsForExistingClient.Count, accountsForNewClient.Count);
        Assert.All(accountsForExistingClient,
            account =>
            {
                Assert.Contains(accountsForNewClient,
                    a => a.Currency.Name == account.Currency.Name && a.Amount == account.Amount);
            });
    }

    [Fact]
    public void TestEmployeeListEquivalence()
    {
        // Arrange 
        var dataGenerator = new TestDataGenerator();
        var employees = dataGenerator.GenerateEmployees(10);
        var existingEmployee = employees[0];
        var newEmployeeWithOverride = new Employee
        {
            Name = existingEmployee.Name,
            Surname = existingEmployee.Surname,
            Age = existingEmployee.Age,
            PhoneNumber = existingEmployee.PhoneNumber,
            Expirence = existingEmployee.Expirence,
            Salary = existingEmployee.Salary
        };

        // Act 
        var containsNewEmployee = employees.Contains(newEmployeeWithOverride);

        // Assert 
        Assert.True(containsNewEmployee);
    }
}