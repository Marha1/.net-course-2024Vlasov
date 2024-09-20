using System.Data;
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