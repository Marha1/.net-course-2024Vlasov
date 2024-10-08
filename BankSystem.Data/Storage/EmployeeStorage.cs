/*using BankSystem.App.Exceptions;
using BankSystemDomain.Models;

namespace BankSystem.Data.Storage;

public class EmployeeStorage
{
    private readonly List<Employee> _employees = new();

    public void AddEmployee(Employee employee)
    {
        if (_employees.Any(e => e.PhoneNumber == employee.PhoneNumber))
            throw new EmployeeAlreadyExistsException();

        if (employee.Age < 18) throw new AgeException();

        if (string.IsNullOrEmpty(employee.PassportDetails)) throw new PassportException();

        _employees.Add(employee);
    }

    public IReadOnlyList<Employee> GetEmployees()
    {
        return _employees.ToList().AsReadOnly();
    }

    public bool RemoveEmployee(string phoneNumber)
    {
        var employeeToRemove = _employees.FirstOrDefault(e => e.PhoneNumber == phoneNumber);
        if (employeeToRemove == null) throw new Exception($"Сотрудник с номером телефона {phoneNumber} не найден.");

        _employees.Remove(employeeToRemove);
        return true;
    }

    public Employee GetOldestEmployee()
    {
        return FindOldestEmployee();
    }

    public Employee GetYoungestEmployee()
    {
        return FindYoungestEmployee();
    }

    public List<Employee> GetEmployeesByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return GetEmployees()
            .Where(employee =>
                (string.IsNullOrEmpty(name) || employee.Name.Contains(name) || employee.Surname.Contains(name)) &&
                (string.IsNullOrEmpty(phoneNumber) || employee.PhoneNumber == phoneNumber) &&
                (string.IsNullOrEmpty(passportDetails) || employee.PassportDetails == passportDetails) &&
                (!birthDateFrom.HasValue || employee.BirthDate >= birthDateFrom) &&
                (!birthDateTo.HasValue || employee.BirthDate <= birthDateTo))
            .Select(employee => new Employee 
            {
                Name = employee.Name,
                Surname = employee.Surname,
                BirthDate = employee.BirthDate,
                PhoneNumber = employee.PhoneNumber,
                PassportDetails = employee.PassportDetails,
                Age = employee.Age
            })
            .ToList();
    }
    private Employee FindOldestEmployee()
    {
        return _employees
            .OrderBy(employee => employee.BirthDate)
            .FirstOrDefault();
    }

    private Employee FindYoungestEmployee()
    {
        return _employees
            .OrderByDescending(employee => employee.BirthDate)
            .FirstOrDefault();
    }

    public bool UpdateEmployee(string phoneNumber, Employee updatedEmployee)
    {
        var employeeToUpdate = _employees.FirstOrDefault(e => e.PhoneNumber == phoneNumber);
        if (employeeToUpdate == null) throw new Exception($"Сотрудник с номером телефона {phoneNumber} не найден.");

        if (updatedEmployee.Age < 18) throw new AgeException("Сотруднику должно быть не менее 18 лет.");

        _employees.Remove(employeeToUpdate);
        _employees.Add(updatedEmployee);
        return true;
    }
}*/