using BankSystem.App.Services;
using BankSystemDomain.Models;

namespace Practice;

public  class Program
{
     static void Main(string[] args)
     {
         Employee employee = new Employee() { 
             Name = "Игорь", 
             Expirence = 10, 
             NumberOfPhone = "7731313",
             Age = 30,
             Surname = "Васькин"
         };
        UpdateContract(employee);
        Console.WriteLine(employee.Contract);
        Currency rub = new Currency()
        {
            Name = "Рубль"
        };
        UpdateCurrency(ref rub,"Доллар");
        Console.WriteLine(rub.Name);
        BankService bankService = new BankService();

        int salary = bankService.CalculateOwnerSalary(1000000, 500000, 5);
        Console.WriteLine($"Зарплата владельца банка: {salary}");

        Client client = new Client { Name = "Рома", Surname = "Никол", Age = 25 }; 
        employee = bankService.ConvertClientToEmployee(client);
        Console.WriteLine($"Новый сотрудник: {employee.Name} {employee.Surname}, возраст: {employee.Age}, зарплата: {employee.Salary}");
        Console.WriteLine(employee.Name);
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