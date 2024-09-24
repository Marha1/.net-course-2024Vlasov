using BankSystemDomain.Models;
using Faker;

namespace BankSystem.App.Services;
/*
 * В папке “Services” реализовать сервис «TestDataGenerator»,
предоставляющий методы:
а) генерации коллекции 1000 клиентов банка;
б) генерации словаря в качестве ключа которого применяется номер
телефона клиента, в качестве значения сам клиент;
в) генерации коллекции 1000 сотрудников банка.
Для запуска методов применяем консольное приложение “Practice”, в
методе «Main» которого, осуществить:
а) пользуясь инструментом “Stopwatch”, провести замер времени
выполнения поиска клиента по его номеру телефона среди элементов
списка;
б) провести замер времени выполнения поиска клиента по его
номеру телефона, среди элементов словаря;
в) выборку клиентов, возраст которых ниже определенного значения;
г) поиск сотрудника с минимальной заработной платой;
33
д) сравнить скорость поиска по словарю двумя методами:
1) при помощи метода FirstOrDefault(ищем последний элемент
коллекции);
2) при помощи выборки по ключу последнего элемента
коллекции
 */
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