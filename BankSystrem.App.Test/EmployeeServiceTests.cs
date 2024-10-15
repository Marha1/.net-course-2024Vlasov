using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using Xunit;

public class EmployeeServiceTests
{
    private readonly BankSystemDbContext _context;
    private readonly TestDataGenerator _dataGenerator;
    private readonly EmployeService _employeeService;
    private readonly EmployeeStorage _storage;

    public EmployeeServiceTests()
    {
        _context = new BankSystemDbContext();
        _storage = new EmployeeStorage(_context);
        _employeeService = new EmployeService(_storage);
        _dataGenerator = new TestDataGenerator();
    }

    [Fact]
    public void AddEmployee_Success_Test()
    {
        // Arrange
        var employee = _dataGenerator.GenerateEmployees(1).First();
        employee.PassportDetails = "987654321";
        // Act
        _employeeService.Add(employee);

        // Assert
        var addedEmployee = _employeeService.GetById(employee.Id);
        Assert.NotNull(addedEmployee);
    }

    [Fact]
    public void DeleteEmployee_Success_Test()
    {
        // Arrange
        var employee = _dataGenerator.GenerateEmployees(1).First();
        employee.PassportDetails = "987654321";
        _employeeService.Add(employee);

        // Act
        var result = _employeeService.Delete(employee);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void UpdateEmployee_Success_Test()
    {
        // Arrange
        var employee = _dataGenerator.GenerateEmployees(1).First();
        employee.PassportDetails = "987654321";
        _employeeService.Add(employee);

        employee.Name = "Updated Name";

        // Act
        var result = _employeeService.Update(employee);

        // Assert
        Assert.True(result);
    }
}