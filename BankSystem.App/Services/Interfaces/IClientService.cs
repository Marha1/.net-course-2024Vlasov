using BankSystemDomain.Models;

namespace BankSystem.App.Services.Interfaces;

public interface IClientService : IBaseService<Client>
{
    Task AddAccountAsync(Guid id, Account newAccount);
    Task<bool> UpdateAccountAsync(Guid id, Account updatedAccount,CancellationToken cancellationToken);
    Task<bool> DeleteAccountAsync(Guid id, Guid currencyId);
    Task<List<Account>> GetAccountsByClientAsync(Client client);
    Task WithdrawAsync(Dictionary<Guid, decimal> withdrawalRequests, CancellationToken cancellationToken);
}