using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Interfaces;

public interface IClientStorage: IBaseStorage<Client>
{
    public void AddAccount(Guid id, Account newAccount);
    public bool UpdateAccount(Guid Id, Account updatedAccount);
    public bool DeleteAccount(Guid Id, Guid currencyId);
    public List<Account> GetAccountsByClient(Client client);
}