using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.App.Services.Storage;
using BankSystemDomain.Models;
using Xunit;

namespace BankSystem.App.Test
{
    public class EmployeeServiceTests
    {
        private readonly TestDataGenerator _dataGenerator;

        public EmployeeServiceTests()
        {
            _dataGenerator = new TestDataGenerator();
        }

        [Fact]
        public void AddEmployee_ThrowsExceptionIfUnder18_Test()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employee = _dataGenerator.GenerateEmployees(1).First();
            employee.Age = 15;

            // Act & Assert
            var exception = Assert.Throws<AgeException>(() => storage.AddEmployee(employee));
            Assert.Equal("Моложе 18 лет!", exception.Message);
        }

        [Fact]
        public void AddEmployee_ThrowsExceptionIfNoPassport_Test()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employee = _dataGenerator.GenerateEmployees(1).First();
            employee.PassportDetails = null;

            // Act & Assert
            var exception = Assert.Throws<PassportException>(() => storage.AddEmployee(employee));
            Assert.Equal("Паспортные данные отсутствуют.", exception.Message);
        }
        
        [Fact]
        public void GetEmployeesByName_Test()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employee1 = _dataGenerator.GenerateEmployees(1).First();
            var employee2 = _dataGenerator.GenerateEmployees(1).First();
            storage.AddEmployee(employee1);
            storage.AddEmployee(employee2);
            var service = new EmployeeService(storage);

            // Act
            var result = service.GetEmployeesByFilter(employee1.Name, null, null, null, null);

            // Assert
            Assert.Contains(employee1, result);
            Assert.DoesNotContain(employee2, result);
        }

        [Fact]
        public void GetYoungestEmployee_Test()
        {
            // Arrange
            var storage = new EmployeeStorage();

            var employee1 = new Employee
            {
                Name = "Кайла",
                BirthDate = new DateTime(1990, 1, 1)
            };
            employee1.Age = 19;
            employee1.PassportDetails = "3213212";
            employee1.PhoneNumber = "saa";
            
            var employee2 = new Employee
            {
                Name = "Циля",
                BirthDate = new DateTime(2000, 1, 1)
            };
            employee2.Age = 20;
            employee2.PassportDetails = "3213212";
            employee2.PhoneNumber = "3sdadsaa";
            storage.AddEmployee(employee1);
            storage.AddEmployee(employee2);
            var service = new EmployeeService(storage);

            // Act
            var youngestEmployee = service.GetYoungestEmployee();

            // Assert
            Assert.Equal(employee2, youngestEmployee);
        }

        [Fact]
        public void GetOldestEmployee_Test()
        {
            // Arrange
            var storage = new EmployeeStorage();

            var employee1 = new Employee
            {
                Name = "Кайла",
                BirthDate = new DateTime(1990, 1, 1)
            };
            employee1.Age = 19;
            employee1.PassportDetails = "3213212";
            employee1.PhoneNumber = "saa";
            
            var employee2 = new Employee
            {
                Name = "Циля",
                BirthDate = new DateTime(2000, 1, 1)
            };
            employee2.Age = 20;
            employee2.PassportDetails = "3213212";
            employee2.PhoneNumber = "3sdadsaa";

            storage.AddEmployee(employee1);
            storage.AddEmployee(employee2);
            var service = new EmployeeService(storage);

            // Act
            var oldestEmployee = service.GetOldestEmployee();

            // Assert
            Assert.Equal(employee1, oldestEmployee);
        }
    }
}
