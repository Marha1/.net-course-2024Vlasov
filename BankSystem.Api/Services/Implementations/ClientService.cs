using AutoMapper;
using BankSystem.Api.Dto.ClientDto;
using BankSystem.Api.Services.Interfaces;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.Api.Services.Implementations
{
    public class ClientService : BaseService<Client, ClientResponse>, IClientService
    {
        private readonly IClientStorage _clientStorage;

        public ClientService(IClientStorage clientStorage, IMapper mapper) 
            : base(clientStorage, mapper)
        {
            _clientStorage = clientStorage;
        }

        public async Task AddAccountAsync(Guid id, Account newAccount)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id не может быть пустым.", nameof(id));
            if (newAccount == null) throw new ArgumentNullException(nameof(newAccount), "Счет не может быть null.");

            await _clientStorage.AddAccountAsync(id, newAccount);
        }

        public async Task<bool> UpdateAccountAsync(Guid id, Account updatedAccount, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id не может быть пустым.", nameof(id));
            if (updatedAccount == null) throw new ArgumentNullException(nameof(updatedAccount), "Счет не может быть null.");

            return await _clientStorage.UpdateAccountAsync(id, updatedAccount, cancellationToken);
        }

        public async Task<bool> DeleteAccountAsync(Guid id, Guid currencyId)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id не может быть пустым.", nameof(id));

            return await _clientStorage.DeleteAccountAsync(id, currencyId);
        }

        public async Task<List<Account>> GetAccountsByClientAsync(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");

            return await _clientStorage.GetAccountsByClientAsync(client);
        }

        public async Task WithdrawAsync(Dictionary<Guid, decimal> withdrawalRequests, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            foreach (var request in withdrawalRequests)
            {
                Guid clientId = request.Key;
                decimal amountToWithdraw = request.Value;
                tasks.Add(Task.Run(async () =>
                {
                    var account = await _clientStorage.GetAccountByIdAsync(clientId, cancellationToken);
                    if (account != null && account.Amount >= amountToWithdraw)
                    {
                        account.Amount -= amountToWithdraw;
                        await _clientStorage.UpdateAccountAsync(account.Id, account, cancellationToken);
                    }
                }, cancellationToken));
            }
            await Task.WhenAll(tasks);
        }
    }
}