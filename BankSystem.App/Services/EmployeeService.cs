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

    public List<Employee> GetEmployeeByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return _employeeStorage.GetEmployeesByFilter(name, phoneNumber, passportDetails, birthDateFrom, birthDateTo);
    }
}