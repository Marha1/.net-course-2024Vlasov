using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Implementations;

public class ClientStorage : BaseStorage<Client>, IClientStorage
{
    private readonly  Dictionary<Client, List<Account>> _clientAccounts;
    
    public ClientStorage()
    {
        _clientAccounts = new Dictionary<Client, List<Account>>();
    }
    
    public List<Client> GetByFilter(string? name, string? phoneNumber, string? passportDetails, DateTime? birthDateFrom,
        DateTime? birthDateTo, int pageNumber, int pageSize)
    {
        var query = GetEntities(1,2).AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(client => client.Name.Contains(name) || client.Surname.Contains(name));
        }

        if (!string.IsNullOrEmpty(phoneNumber))
        {
            query = query.Where(client => client.PhoneNumber == phoneNumber);
        }

        if (!string.IsNullOrEmpty(passportDetails))
        {
            query = query.Where(client => client.PassportDetails == passportDetails);
        }

        if (birthDateFrom.HasValue)
        {
            query = query.Where(client => client.BirthDate >= birthDateFrom);
        }

        if (birthDateTo.HasValue)
        {
            query = query.Where(client => client.BirthDate <= birthDateTo);
        }

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return query.ToList();    
    }

    public void AddAccount(string phoneNumber, Account newAccount)
    {
        var client = GetEntities(1, 10).FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (client == null)
        {
            throw new Exception($"Клиент с номером телефона {phoneNumber} не найден.");
        }

        if (!_clientAccounts.ContainsKey(client)|| _clientAccounts.Count==0)
        {
            _clientAccounts[client] = new List<Account>();
        }

        var existingAccount = _clientAccounts[client].FirstOrDefault(a => a.Currency.Name == newAccount.Currency.Name);
        if (existingAccount != null)
        {
            throw new Exception($"Счёт с валютой {newAccount.Currency.Name} уже существует для клиента.");
        }

        _clientAccounts[client].Add(newAccount);
    }


    public bool UpdateAccount(string phoneNumber, Account updatedAccount)
        {
            var client = _clientAccounts.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            if (client == null)
            {
                throw new Exception($"Клиент с номером телефона {phoneNumber} не найден.");
            }

            var accountToUpdate = _clientAccounts[client].FirstOrDefault(a => a.Currency.Name == updatedAccount.Currency.Name);
            if (accountToUpdate == null)
            {
                throw new Exception($"Счёт с валютой {updatedAccount.Currency.Name} не найден для клиента.");
            }

            accountToUpdate.Amount = updatedAccount.Amount;
            accountToUpdate.Currency = updatedAccount.Currency;
            return true;
        }

    public bool DeleteAccount(string phoneNumber, string currency)
    {
        var client = _clientAccounts.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (client == null)
        {
            throw new Exception($"Клиент с номером телефона {phoneNumber} не найден.");
        }

        var accountToRemove = _clientAccounts[client].FirstOrDefault(a => a.Currency.Name == currency);
        if (accountToRemove == null)
        {
            throw new Exception("Счет не найден.");
        }

        _clientAccounts[client].Remove(accountToRemove);
        return true;
    }
    public List<Account> GetAccountsByPhoneNumber(string phoneNumber)
    {
        var client = _clientAccounts.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (client == null)
        {
            throw new Exception($"Клиент с номером телефона {phoneNumber} не найден.");
        }

        return _clientAccounts[client];
    }
}