using BankSystemDomain.Models;

namespace BankSystem.App.Services;

public class BankService
{
    public int CalculateOwnerSalary(int bankProfit, int bankExpenses, int ownerCount)
    {
        return (bankProfit - bankExpenses) / ownerCount;
    }

    public Employee ConvertClientToEmployee(Client client)
    {
        if (client is Employee)
        {
            return (Employee)(object)client;
        }

        return new Employee
        {
            Name = client.Name,
            Surname = client.Surname,
            Age = client.Age,
            Expirence = 0, 
            Salary = 3000
        };
    }
}
