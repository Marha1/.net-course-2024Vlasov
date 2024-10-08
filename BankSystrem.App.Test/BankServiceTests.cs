using BankSystem.App.Services;
using BankSystem.App.Services.Implementations;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;
using Xunit;

namespace BankSystem.App.Tests
{
    public class BankServiceTests
    {
        private readonly BankService _bankService;
        private readonly ClientService _clientService;

        public BankServiceTests()
        {
            _bankService = new BankService();
           var _storage = new ClientStorage();
            _clientService = new ClientService(_storage);
            
        }

        [Fact]
        public void AddBonus_ShouldAddBonusToClientAccount()
        {
            // Arrange
            var client = new Client { PhoneNumber = "12345" };
            var account = new Account { Amount = 0 };
            client.Age = 19;
            client.PassportDetails = "dsad";
            _clientService.Add(client);
            _clientService.AddAccountToClient(client.PhoneNumber,account);
            // Assert
            var bonusAccount = _clientService.GetAccountsByPhoneNumber(client.PhoneNumber).FirstOrDefault();
            bonusAccount.Amount = 100;
            Assert.NotNull(bonusAccount);
        }

        [Fact]
        public void AddBonus_ShouldIncreaseEmployeeSalary()
        {
            // Arrange
            var employee = new Employee { Salary = 500 };

            // Act
            _bankService.AddBonus(employee, 100);

            // Assert
            Assert.Equal(600, employee.Salary);
        }

        [Fact]
        public void AddBonus_ShouldThrowExceptionForUnknownPerson()
        {
            // Arrange
            var unknownPerson = new Person { Name = "Unknown", Age = 30 };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _bankService.AddBonus(unknownPerson, 100));
        }

        [Fact]
        public void AddToBlackList_ShouldAddPersonToBlackList()
        {
            // Arrange
            var person = new Client { Name = "TestClient" };

            // Act
            _bankService.AddToBlackList(person);

            // Assert
            Assert.True(_bankService.IsPersonInBlackList(person));
        }

        [Fact]
        public void IsPersonInBlackList_ShouldReturnFalseIfPersonNotInBlackList()
        {
            // Arrange
            var person = new Client { Name = "TestClient" };

            // Act
            var result = _bankService.IsPersonInBlackList(person);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsPersonInBlackList_ShouldReturnTrueIfPersonInBlackList()
        {
            // Arrange
            var person = new Client { Name = "TestClient" };
            _bankService.AddToBlackList(person);

            // Act
            var result = _bankService.IsPersonInBlackList(person);

            // Assert
            Assert.True(result);
        }
    }
}
