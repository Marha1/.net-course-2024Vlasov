using BankSystem.App.Services.Interfaces;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.App.Services.Implementations;

public class ClientService : BaseService<Client>, IClientService
{
    private readonly IClientStorage _clientStorage;

    public ClientService(IClientStorage clientStorage) : base(clientStorage)
    {
        _clientStorage = clientStorage;
    }

    public void AddAccount(Guid id, Account newAccount)
    {
        try
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым.", nameof(id));

            if (newAccount == null)
                throw new ArgumentNullException(nameof(newAccount), "Счет не может быть null.");

            _clientStorage.AddAccount(id, newAccount);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public bool UpdateAccount(Guid id, Account updatedAccount)
    {
        try
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым.", nameof(id));

            if (updatedAccount == null)
                throw new ArgumentNullException(nameof(updatedAccount), "Счет не может быть null.");

            return _clientStorage.UpdateAccount(id, updatedAccount);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool DeleteAccount(Guid id, Guid currencyId)
    {
        try
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым.", nameof(id));

            return _clientStorage.DeleteAccount(id, currencyId);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<Account> GetAccountsByClient(Client client)
    {
        try
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");

            return _clientStorage.GetAccountsByClient(client);
        }
        catch (Exception)
        {
         
            return new List<Account>(); 
        }
    }

    public Client GetById(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id не может быть пустым.", nameof(id));

            return _clientStorage.GetById(id);
        }
        catch 
        {
            
            return null; 
        }
    }
}
