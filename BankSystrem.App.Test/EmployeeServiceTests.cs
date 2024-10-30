using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

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
    public async Task AddEmployee_Success_Test()
    {
        // Arrange
        var employee = _dataGenerator.GenerateEmployees(1).First();
        employee.PassportDetails = "987654321";

        // Act
        await _employeeService.AddAsync(employee);

        // Assert
        var addedEmployee = await _employeeService.GetByIdAsync(employee.Id);
        Assert.NotNull(addedEmployee);
    }

    [Fact]
    public async Task DeleteEmployee_Success_Test()
    {
        // Arrange
        var employee = _dataGenerator.GenerateEmployees(1).First();
        employee.PassportDetails = "987654321";
        await _employeeService.AddAsync(employee);

        // Act
        var result = await _employeeService.DeleteAsync(employee);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateEmployee_Success_Test()
    {
        // Arrange
        var employee = _dataGenerator.GenerateEmployees(1).First();
        employee.PassportDetails = "987654321";
        await _employeeService.AddAsync(employee);

        employee.Name = "Updated Name";

        // Act
        var result = await _employeeService.UpdateAsync(employee);

        // Assert
        Assert.True(result);
    }
}