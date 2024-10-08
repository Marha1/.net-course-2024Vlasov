/*
using BankSystemDomain.Models;
using BankSystem.App.Exceptions;

namespace BankSystem.Data.Storage
{
    public class ClientStorage
    {
        private readonly Dictionary<Client, List<Account>> _clientAccounts = new();

        public void AddClient(Client client)
        {
            if (_clientAccounts.ContainsKey(client))
            {
                throw new Exception("Клиент уже существует.");
            }

            if (client.Age < 18)
            {
                throw new AgeException();
            }

            if (string.IsNullOrEmpty(client.PassportDetails))
            {
                throw new Exception("Паспортные данные отсутствуют.");
            }

            var defaultAccount = new Account
            {
                Currency = new Currency { Name = "USD" }, 
                Amount = 0
            };

            _clientAccounts.Add(client, new List<Account> { defaultAccount });
        }

        public List<Client> GetClientsByFilter(string? name, string? phoneNumber, string? passportDetails,
            DateTime? birthDateFrom, DateTime? birthDateTo)
        {
            return GetClients()
                .Where(client =>
                    (string.IsNullOrEmpty(name) || client.Key.Name.Contains(name) || client.Key.Surname.Contains(name)) &&
                    (string.IsNullOrEmpty(phoneNumber) || client.Key.PhoneNumber == phoneNumber) &&
                    (string.IsNullOrEmpty(passportDetails) || client.Key.PassportDetails == passportDetails) &&
                    (!birthDateFrom.HasValue || client.Key.BirthDate >= birthDateFrom) &&
                    (!birthDateTo.HasValue || client.Key.BirthDate <= birthDateTo))
                .Select(client => client.Key)
                .ToList();
        }
        public IReadOnlyDictionary<Client, IReadOnlyList<Account>> GetClients()
        {
            return _clientAccounts
                .ToDictionary(
                    client => client.Key,
                    account => (IReadOnlyList<Account>)account.Value.ToList().AsReadOnly());
        }

        public bool RemoveClient(string phoneNumber)
        {
            var clientToRemove = _clientAccounts.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            if (clientToRemove == null)
            {
                throw new Exception($"Клиент с номером телефона {phoneNumber} не найден.");
            }

            _clientAccounts.Remove(clientToRemove);
            return true;
        }

        public Client GetOldestClient()
        {
            return FindOldestClient(); 
        }

        public Client GetYoungestClient()
        {
            return FindYoungestClient(); 
        }

        private Client FindOldestClient()
        {
            return _clientAccounts
                .OrderByDescending(client => client.Key.BirthDate)
                .FirstOrDefault().Key;
        }

        private Client FindYoungestClient()
        {
            return _clientAccounts
                .OrderBy(client => client.Key.BirthDate)
                .FirstOrDefault().Key;
        }
        public bool UpdateClient(string phoneNumber, Client updatedClient)
        {
            var clientToUpdate = _clientAccounts.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            if (clientToUpdate == null)
            {
                throw new Exception($"Клиент с номером телефона {phoneNumber} не найден.");
            }

            if (updatedClient.Age < 18)
            {
                throw new AgeException("Клиенту должно быть не менее 18 лет.");
            }

            _clientAccounts.Remove(clientToUpdate); 
            _clientAccounts.Add(updatedClient, _clientAccounts[clientToUpdate]); 
            return true;
        }

        public void AddAccount(string phoneNumber, Account newAccount)
        {
            var client = _clientAccounts.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            if (client == null)
            {
                throw new Exception($"Клиент с номером телефона {phoneNumber} не найден.");
            }

            var existingAccount = _clientAccounts[client].FirstOrDefault(a => a.Currency.Name == newAccount.Currency.Name);
            if (existingAccount != null)
            {
                throw new Exception($"Счёт с валютой {newAccount.Currency.Name} уже существует для клиента.");
            }

            _clientAccounts[client].Add(newAccount);
        }

        public bool UpdateClientAccount(string phoneNumber, Account updatedAccount)
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
    }
}
*/
