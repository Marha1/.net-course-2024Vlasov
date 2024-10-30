using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storage.Implementations
{
    public class ClientStorage : BaseStorage<Client>, IClientStorage
    {
        private readonly BankSystemDbContext _context;

        public ClientStorage(BankSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task AddAsync(Client client)
        {
            if (await _context.Clients.AnyAsync(e => e.Equals(client)))
            {
                throw new Exception($"Клиент с именем {client.Name} уже существует.");
            }

            var usdCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.Name == "USD");
            if (usdCurrency == null)
            {
                usdCurrency = new Currency { Name = "USD" };
                await _context.Currencies.AddAsync(usdCurrency);
                await _context.SaveChangesAsync(); 
            }

            client.Accounts ??= new List<Account>();

            AddDefaultAccountIfNotExists(client, usdCurrency.Id);

            await base.AddAsync(client);
        }

        private void AddDefaultAccountIfNotExists(Client client, Guid usdCurrencyId)
        {
            if (!client.Accounts.Any(a => a.CurrencyId == usdCurrencyId))
            {
                client.Accounts.Add(new Account
                {
                    Amount = 0,
                    CurrencyId = usdCurrencyId
                });
            }
        }

        public async Task AddAccountAsync(Guid id, Account newAccount)
        {
            var existingClient = await _context.Clients
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingClient == null) throw new Exception("Клиент не найден.");

            if (existingClient.Accounts.Any(a => a.CurrencyId == newAccount.CurrencyId))
                throw new Exception("Счёт с указанной валютой уже существует для клиента.");

            existingClient.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAccountAsync(Guid id, Account updatedAccount,CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingClient == null) throw new Exception("Клиент не найден.");

            var accountToUpdate = existingClient.Accounts.FirstOrDefault(a => a.CurrencyId == updatedAccount.CurrencyId);
            if (accountToUpdate == null) throw new Exception("Счёт с указанной валютой не найден для клиента.");

            accountToUpdate.Amount = updatedAccount.Amount;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken)
        {
            var exAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);

            if (exAccount == null)
                throw new Exception("Account not found.");

            exAccount.Amount = account.Amount;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Account> GetAccountByIdAsync(Guid clientId, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(account => account.Id == clientId, cancellationToken);
        }

        public async Task<bool> DeleteAccountAsync(Guid id, Guid currencyId)
        {
            var existingClient = await _context.Clients
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingClient == null) throw new Exception("Клиент не найден.");

            var accountToRemove = existingClient.Accounts.FirstOrDefault(a => a.CurrencyId == currencyId);
            if (accountToRemove == null) throw new Exception("Счёт не найден.");

            existingClient.Accounts.Remove(accountToRemove);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<ICollection<Account>> GetAllAccount(CancellationToken cancellationToken)
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<List<Account>> GetAccountsByClientAsync(Client client)
        {
            var existingClient = await _context.Clients
                .Include(c => c.Accounts)
                .ThenInclude(a => a.Currency)
                .FirstOrDefaultAsync(c => c.Id == client.Id);

            if (existingClient == null) throw new Exception($"Клиент {client.Name} не найден.");

            return existingClient.Accounts;
        }
    }
}
