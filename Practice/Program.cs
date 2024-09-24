using System.Diagnostics;
using BankSystem.App.Services;
using BankSystemDomain.Models;

namespace Practice;

public class Program
{
    private static void Main(string[] args)
    {
        var generator = new TestDataGenerator();

        var clients = generator.GenerateClients();
        var clientDictionary = generator.GenerateClientDictionary(clients);
        var employees = generator.GenerateEmployees();

        var testPhoneNumber = clients[500].PhoneNumber;

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var clientFromList = clients.FirstOrDefault(c => c.PhoneNumber == testPhoneNumber);
        stopwatch.Stop();
        Console.WriteLine($"Время поиска в списке : {stopwatch.Elapsed.TotalMilliseconds} мс");

        stopwatch.Restart();
        var clientFromDictionary =
            clientDictionary.ContainsKey(testPhoneNumber) ? clientDictionary[testPhoneNumber] : null;
        stopwatch.Stop();
        Console.WriteLine($"Время поиска в словаре : {stopwatch.Elapsed.TotalMilliseconds} мс");

        var ageThreshold = 30;
        var youngClients = clients.Where(c => c.Age < ageThreshold).ToList();
        Console.WriteLine($"Найдено {youngClients.Count} клиентов младше {ageThreshold} лет");

        var employeeWithMinSalary = employees.OrderBy(e => e.Salary).FirstOrDefault();
        Console.WriteLine(
            $"Сотрудник с минимальной зарплатой: {employeeWithMinSalary.Name}, Зарплата: {employeeWithMinSalary.Salary}");

        stopwatch.Restart();
        var lastClientFromList = clients.LastOrDefault(c => c.PhoneNumber == testPhoneNumber);
        stopwatch.Stop();
        Console.WriteLine($"Время поиска с помощью LastOrDefault: {stopwatch.Elapsed.TotalMilliseconds} мс");

        stopwatch.Restart();
        var clientFromDictionaryByKey = clientDictionary[testPhoneNumber];
        stopwatch.Stop();
        Console.WriteLine($"Время поиска в словаре по ключу занял: {stopwatch.Elapsed.TotalMilliseconds} мс");
    }


    private static void OldTaskRun()
    {
        #region Типы значений и Ссылочные типы

        var employee = new Employee
        {
            Name = "Игорь",
            Expirence = 10,
            PhoneNumber = "7731313",
            Age = 30,
            Surname = "Васькин"
        };
        UpdateContract(employee);
        Console.WriteLine(employee.Contract);
        var rub = new Currency
        {
            Name = "Рубль"
        };
        UpdateCurrency(ref rub, "Доллар");
        Console.WriteLine(rub.Name);

        #endregion

        #region Приведение и преобразование Типов

        var bankService = new BankService();

        var salary = bankService.CalculateOwnerSalary(1000000, 500000, 5);
        Console.WriteLine($"Зарплата владельца банка: {salary}");

        var client = new Client { Name = "Рома", Surname = "Никол", Age = 25 };
        employee = bankService.ConvertClientToEmployee(client);
        Console.WriteLine(
            $"Новый сотрудник: {employee.Name} {employee.Surname}, возраст: {employee.Age}, зарплата: {employee.Salary}");
        Console.WriteLine(employee.Name);

        #endregion
    }

    private static void UpdateCurrency(ref Currency rub, string newCureency)
    {
        rub.Name = newCureency;
    }

    public static void UpdateContract(Employee employ)
    {
        employ.Salary = 3000;
        var contract = $"Имя:{employ.Name}\nФамилия:{employ.Surname},\nЗарплата:{employ.Salary}";
        employ.Contract = contract;
    }
}