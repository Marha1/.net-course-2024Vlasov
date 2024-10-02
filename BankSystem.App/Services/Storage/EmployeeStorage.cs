using BankSystemDomain.Models;

namespace BankSystem.App.Services.Storage;

public class EmployeeStorage
{
    private readonly List<Employee> _employees = new();

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
    }

    public List<Employee> GetEmployees()
    {
        return _employees;
    }
    
}