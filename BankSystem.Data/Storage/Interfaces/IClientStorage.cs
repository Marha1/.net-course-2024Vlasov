using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Interfaces;

public interface IClientStorage : IBaseStorage<Client>
{
    Task AddAccountAsync(Guid id, Account newAccount);
    Task<bool> UpdateAccountAsync(Guid id, Account updatedAccount,CancellationToken cancellationToken);
    Task<bool> DeleteAccountAsync(Guid id, Guid currencyId);
    Task<List<Account>> GetAccountsByClientAsync(Client client);
    Task<ICollection<Account>> GetAllAccount(CancellationToken cancellationToken);
    Task<Account> GetAccountByIdAsync(Guid clientId, CancellationToken cancellationToken);
    Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken);
}