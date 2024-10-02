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
        public void AddEmployee()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employee = _dataGenerator.GenerateEmployees(1).First();

            // Act
            storage.AddEmployee(employee);
            var employees = storage.GetEmployees();

            // Assert
            Assert.Contains(employee, employees);
        }

        [Fact]
        public void GetEmployeesByFilter()
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
        public void GetYoungestEmployee()
        {
            // Arrange
            var storage = new EmployeeStorage();

            var employee1 = new Employee
            {
                Name = "Кайла",
                BirthDate = new DateTime(1990, 1, 1) 
            };

            var employee2 = new Employee
            {
                Name = "Циля",
                BirthDate = new DateTime(2000, 1, 1) 
            };

            storage.AddEmployee(employee1);
            storage.AddEmployee(employee2);
            var service = new EmployeeService(storage);

            // Act
            var youngestEmployee = service.GetYoungestEmployee();

            // Assert
            Assert.Equal(employee2, youngestEmployee); 
        }

        [Fact]
        public void GetOldestEmployee()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employee1 = _dataGenerator.GenerateEmployees(1).First();
            var employee2 = _dataGenerator.GenerateEmployees(1).First();
            storage.AddEmployee(employee1);
            storage.AddEmployee(employee2);
            var service = new EmployeeService(storage);

            // Act
            var oldestEmployee = service.GetOldestEmployee();

            // Assert
            Assert.Equal(employee1, oldestEmployee);
        }

        [Fact]
        public void CalculateAverageAge()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employee1 = _dataGenerator.GenerateEmployees(1).First();
            var employee2 = _dataGenerator.GenerateEmployees(1).First();
            storage.AddEmployee(employee1);
            storage.AddEmployee(employee2);
            var service = new EmployeeService(storage);

            // Act
            var averageAge = service.CalculateAverageAge();

            // Assert
            Assert.Equal((employee1.Age + employee2.Age) / 2, averageAge);
        }
    }
}
