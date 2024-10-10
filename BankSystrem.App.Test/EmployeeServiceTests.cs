using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.App.Services.Interfaces;
using BankSystem.Data.Storage;
using BankSystem.Data.Storage.Implementations;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;
using Xunit;

namespace BankSystem.App.Tests
{
    public class EmployeeServiceTests
    {
        private readonly EmployeService _employService;
        private readonly TestDataGenerator _dataGenerator;
        private readonly EmployeeStorage _employeeStorage;
        public EmployeeServiceTests()
        {
            _dataGenerator = new TestDataGenerator();
            _employeeStorage = new EmployeeStorage();
            _employService = new EmployeService(_employeeStorage);
        }

        [Fact]
        public void AddEmployee_Success_Test()
        {
            // Arrange
            
            var employee = _dataGenerator.GenerateEmployees(1).First();

            // Act
            _employService.Add(employee);

            // Assert
            var employees = _employeeStorage.GetEntities(1, 100); 
            Assert.Contains(employee, employees);
        }

        [Fact]
        public void UpdateEmployee_Success_Test()
        {
            // Arrange
            var employee = _dataGenerator.GenerateEmployees(1).First();
            _employService.Add(employee);

            var updatedEmployee = new Employee
            {
                Name = "Гыг",
                Surname = employee.Surname,
                BirthDate = employee.BirthDate,
                PassportDetails = employee.PassportDetails,
                PhoneNumber = employee.PhoneNumber,
                Age = employee.Age,
            };

            // Act
            var result = _employService.Update(updatedEmployee);

            // Assert
            var updated = _employService.GetEntities(1, 100) 
                .FirstOrDefault(e => e.Name == updatedEmployee.Name);

            Assert.True(result);
            Assert.NotNull(updated); 
            Assert.Equal("Гыг", updated.Name);
        }


        [Fact]
        public void DeleteEmployee_Success_Test()
        {
            // Arrange
            var employee = _dataGenerator.GenerateEmployees(1).First();
            _employService.Add(employee);

            // Act
            var result = _employService.Delete(employee);

            // Assert
            var employees = _employeeStorage.GetEntities(1,5);
            Assert.DoesNotContain(employee,employees);
        }
    }
}
