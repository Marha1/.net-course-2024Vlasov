using BankSystemDomain.Models;

namespace BankSystem.App.Services.Interfaces;

public interface IClientService : IBaseService<Client>
{
    public void AddAccount(Guid id, Account newAccount);
    public bool UpdateAccount(Guid id, Account updatedAccount);
    public bool DeleteAccount(Guid id, Guid currencyId);
    public List<Account> GetAccountsByClient(Client client);
}