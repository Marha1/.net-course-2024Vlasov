using BankSystem.App.Services.Storage;
using BankSystemDomain.Models;

namespace BankSystem.App.Services;

public class EmployeeService
{
    private readonly EmployeeStorage _employeeStorage;

    public EmployeeService(EmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }

    public void AddEmployee(Employee employee)
    {
        _employeeStorage.AddEmployee(employee);
    }

    public bool RemoveEmployee(string phoneNumber)
    {
        return _employeeStorage.RemoveEmployee(phoneNumber);
    }

    public bool UpdateEmployee(string phoneNumber, Employee updatedEmployee)
    {
        return _employeeStorage.UpdateEmployee(phoneNumber, updatedEmployee);
    }

    public List<Employee> GetEmployeesByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return _employeeStorage.GetEmployeesByFilter(name, phoneNumber, passportDetails, birthDateFrom, birthDateTo);
    }

    public Employee GetOldestEmployee()
    {
        return _employeeStorage.GetOldestEmployee();
    }

    public Employee GetYoungestEmployee()
    {
        return _employeeStorage.GetYoungestEmployee();
    }

    public double CalculateAverageAge()
    {
        var employees = _employeeStorage.GetEmployees();
        return employees.Count > 0
            ? employees.Average(employee => employee.Age)
            : 0;
    }
}