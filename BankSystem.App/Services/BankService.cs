using BankSystem.App.Services.Implementations;
using BankSystem.Data.Storage.Implementations;
using BankSystemDomain.Models;

namespace BankSystem.App.Services;

public class BankService
{
    private readonly List<Person> _blackList = new();
    private readonly ClientService _clientService;
    private readonly ClientStorage _storage;

    public BankService()
    {
        _storage = new ClientStorage();
        _clientService = new ClientService(_storage);
    }

    public void AddBonus(Person person, decimal bonusAmount)
    {
        if (person is Client client)
        {
            var account = _clientService.GetAccountsByPhoneNumber(person.PhoneNumber)
                .FirstOrDefault();

            if (account != null) account.Amount = 100;
        }
        else if (person is Employee employee)
        {
            employee.Salary += bonusAmount;
        }
        else
        {
            throw new InvalidOperationException("Неизвестный тип для добавления бонуса.");
        }
    }

    public void AddToBlackList<T>(T person) where T : Person
    {
        if (!_blackList.Contains(person)) _blackList.Add(person);
    }

    public bool IsPersonInBlackList<T>(T person) where T : Person
    {
        return _blackList.Contains(person);
    }
}