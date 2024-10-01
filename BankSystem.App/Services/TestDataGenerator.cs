using BankSystemDomain.Models;
using Faker;
using Currency = BankSystemDomain.Models.Currency;

namespace BankSystem.App.Services;

public class TestDataGenerator
{
    private static readonly Random random = new();

    public List<Client> GenerateClients(int count = 1000)
    {
        var clients = new List<Client>();
        for (var i = 0; i < count; i++)
        {
            var age = random.Next(18, 65);
            var year = DateTime.Today.Year - age; 
            var month = random.Next(1, 13); 
            var day = random.Next(1, DateTime.DaysInMonth(year, month) + 1); 
            clients.Add(new Client
            {

                Name = NameFaker.FirstName(),
                Surname = NameFaker.LastName(),
                Age = age,
                PhoneNumber = GeneratePhoneNumber(),
                BirthDate = new DateTime(year, month, day),
            });
        }

        return clients;
    }

    public Dictionary<Client, List<Account>> GenerateClientAccounts(List<Client> clients)
    {
        return clients.ToDictionary(
            client => client,
            client => new List<Account>
            {
                new Account { Currency = new Currency { Name = "USD" }, Amount = (decimal)(random.Next(1000, 10000) + random.NextDouble()) },
                new Account { Currency = new Currency { Name = "Rub" }, Amount = (decimal)(random.Next(1000, 100000) + random.NextDouble()) }
            });
    }


    public Dictionary<string, Client> GenerateClientDictionary(List<Client> clients)
    {
        return clients.ToDictionary(c => c.PhoneNumber, c => c);
    }
    

    public List<Employee> GenerateEmployees(int count = 1000)
    {
        var employees = new List<Employee>();
        for (var i = 0; i < count; i++)
        {
            var age = random.Next(18, 65);
            var year = DateTime.Today.Year - age; 
            var month = random.Next(1, 13); 
            var day = random.Next(1, DateTime.DaysInMonth(year, month) + 1); 
            employees.Add(new Employee
            {
                Name = NameFaker.FirstName(),
                Surname = NameFaker.LastName(),
                Age = age,
                BirthDate = new DateTime(year, month, day),
                PhoneNumber = GeneratePhoneNumber()
            });
        }

        return employees;
    }

    private string GeneratePhoneNumber()
    {
        return $"77{random.Next(10000000, 99999999)}";
    }
}
