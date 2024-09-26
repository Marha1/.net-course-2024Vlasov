using BankSystemDomain.Models;
using Faker;

namespace BankSystem.App.Services;

public class TestDataGenerator
{
    private static readonly Random random = new();

    public List<Client> GenerateClients(int count = 1000)
    {
        var clients = new List<Client>();

        for (var i = 0; i < count; i++)
            clients.Add(new Client
            {
                Name = NameFaker.FirstName(),
                Surname = NameFaker.LastName(),
                Age = random.Next(18, 65),
                PhoneNumber = GeneratePhoneNumber()
            });

        return clients;
    }

    public Dictionary<string, Client> GenerateClientDictionary(List<Client> clients)
    {
        return clients.ToDictionary(c => c.PhoneNumber, c => c);
    }

    public List<Employee> GenerateEmployees(int count = 1000)
    {
        var employees = new List<Employee>();

        for (var i = 0; i < count; i++)
            employees.Add(new Employee
            {
                Name = NameFaker.FirstName(),
                Surname = NameFaker.LastName(),
                Age = random.Next(18, 65),
                PhoneNumber = GeneratePhoneNumber(),
                Expirence = random.Next(1, 40),
                Salary = (decimal)(random.Next(30000, 100000) + random.NextDouble())
            });

        return employees;
    }

    private string GeneratePhoneNumber()
    {
        return $"77{random.Next(10000000, 99999999)}";
    }
}