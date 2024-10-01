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

    public List<Employee> GetEmployeesByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return _employeeStorage.GetEmployees().Where(employee =>
            (string.IsNullOrEmpty(name) || employee.Name.Contains(name) || employee.Surname.Contains(name)) &&
            (string.IsNullOrEmpty(phoneNumber) || employee.PhoneNumber == phoneNumber) &&
            (string.IsNullOrEmpty(passportDetails) || employee.PassportDetails == passportDetails) &&
            (!birthDateFrom.HasValue || employee.BirthDate >= birthDateFrom) &&
            (!birthDateTo.HasValue || employee.BirthDate <= birthDateTo)
        ).ToList();
    }

    public Employee GetYoungestEmployee()
    {
        return _employeeStorage.GetEmployees().OrderByDescending(employee => employee.BirthDate).FirstOrDefault();
    }

    public Employee GetOldestEmployee()
    {
        return _employeeStorage.GetEmployees().OrderBy(employee => employee.BirthDate).FirstOrDefault();
    }

    public double CalculateAverageAge()
    {
        return _employeeStorage.GetEmployees().Average(employee => employee.Age);
    }
}