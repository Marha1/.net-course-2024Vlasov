using BankSystemDomain.Models;

namespace BankSystem.App.Services.Storage;

public class EmployeeStorage
{
    private readonly List<Employee> _employees = new();

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
    }
    public List<Employee> GetEmployeesByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return _employees.Where(employee =>
            (string.IsNullOrEmpty(name) || employee.Name.Contains(name) || employee.Surname.Contains(name)) &&
            (string.IsNullOrEmpty(phoneNumber) || employee.PhoneNumber == phoneNumber) &&
            (string.IsNullOrEmpty(passportDetails) || employee.PassportDetails == passportDetails) &&
            (!birthDateFrom.HasValue || employee.BirthDate >= birthDateFrom) &&
            (!birthDateTo.HasValue || employee.BirthDate <= birthDateTo)
        ).ToList();
    }
    public IReadOnlyList<Employee> GetEmployee()
    {
        return _employees.AsReadOnly();
    }
    public Employee GetYoungestEmployee()
    {
        return _employees.OrderByDescending(employee => employee.BirthDate).FirstOrDefault();
    }

    public Employee GetOldestEmployee()
    {
        return _employees.OrderBy(employee => employee.BirthDate).FirstOrDefault();
    }

    public double CalculateAverageAge()
    {
        return _employees.Average(employee => employee.Age);
    }
    
}