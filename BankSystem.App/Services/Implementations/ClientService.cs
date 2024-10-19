using BankSystem.App.Exceptions;
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

    public override void Add(Client client)
    {
        if (client.Age < 18)
            throw new AgeException("Возраст клиента должен быть не менее 18 лет.");
        
        if (string.IsNullOrWhiteSpace(client.PassportDetails))
            throw new PassportException("Паспортные данные клиента отсутствуют.");

        base.Add(client);
    }
    public void AddAccount(Guid id, Account newAccount)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id не может быть пустым.", nameof(id));

        if (newAccount == null)
            throw new ArgumentNullException(nameof(newAccount), "Счет не может быть null.");

        _clientStorage.AddAccount(id, newAccount);
    }

    public bool UpdateAccount(Guid id, Account updatedAccount)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id не может быть пустым.", nameof(id));

        if (updatedAccount == null)
            throw new ArgumentNullException(nameof(updatedAccount), "Счет не может быть null.");

        return _clientStorage.UpdateAccount(id, updatedAccount);
    }

    public bool DeleteAccount(Guid id, Guid currencyId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id не может быть пустым.", nameof(id));

        return _clientStorage.DeleteAccount(id, currencyId);
    }

    public List<Account> GetAccountsByClient(Client client)
    {
        if (client == null)
            throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");

        return _clientStorage.GetAccountsByClient(client);
    }
}