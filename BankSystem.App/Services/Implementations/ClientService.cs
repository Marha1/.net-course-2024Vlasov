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

    public List<Client> GetClientsByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo, int pageNumber, int pageSize)
    {
        return _clientStorage.GetByFilter(name, phoneNumber, passportDetails, birthDateFrom, birthDateTo, pageNumber, pageSize);
    }

    public void AddAccountToClient(string phoneNumber, Account account)
    {
        _clientStorage.AddAccount(phoneNumber, account);
    }

    public bool UpdateClientAccount(string phoneNumber, Account account)
    {
        return _clientStorage.UpdateAccount(phoneNumber, account);
    }

    public bool DeleteClientAccount(string phoneNumber, string currency)
    {
        return _clientStorage.DeleteAccount(phoneNumber, currency);
        
    }
    public List<Account> GetAccountsByPhoneNumber(string phoneNumber)
    {
        return _clientStorage.GetAccountsByPhoneNumber(phoneNumber);
    }
}