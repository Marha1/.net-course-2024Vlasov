using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storage.Implementations;

public class ClientStorage : BaseStorage<Client>, IClientStorage
{
    private readonly BankSystemDbContext _context;

    public ClientStorage(BankSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public override void Add(Client client)
    {
        if (_context.Clients.Any(e => e.Equals(client)))
        {
            throw new Exception($"Клиент с именем {client.Name} уже существует.");
        }

        var usdCurrency = _context.Currencies.FirstOrDefault(c => c.Name == "USD");
        if (usdCurrency == null)
        {
            usdCurrency = new Currency { Name = "USD" };
            _context.Currencies.Add(usdCurrency);
            _context.SaveChanges(); 
        }

        if (client.Accounts == null)
        {
            client.Accounts = new List<Account>();
        }

        AddDefaultAccountIfNotExists(client, usdCurrency.Id);

        base.Add(client);
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

    public void AddAccount(Guid id, Account newAccount)
    {
        var existingClient = _context.Clients
            .Include(c => c.Accounts)
            .FirstOrDefault(c => c.Id == id);

        if (existingClient == null) throw new Exception($"Клиент не найден.");

        var existingAccount = existingClient.Accounts.FirstOrDefault(a => a.CurrencyId == newAccount.CurrencyId);
        if (existingAccount != null) 
            throw new Exception("Счёт с указанной валютой уже существует для клиента.");

        existingClient.Accounts.Add(newAccount);
        _context.SaveChanges();
    }

    public bool UpdateAccount(Guid Id, Account updatedAccount)
    {
        var existingClient = _context.Clients
            .Include(c => c.Accounts)
            .FirstOrDefault(c => c.Id == Id);

        if (existingClient == null) throw new Exception($"Клиент не найден.");

        var accountToUpdate = existingClient.Accounts.FirstOrDefault(a => a.CurrencyId == updatedAccount.CurrencyId);
        if (accountToUpdate == null) throw new Exception("Счёт с указанной валютой не найден для клиента.");

        accountToUpdate.Amount = updatedAccount.Amount;
        _context.SaveChanges();

        return true;
    }

    public bool DeleteAccount(Guid Id, Guid currencyId)
    {
        var existingClient = _context.Clients
            .Include(c => c.Accounts)
            .FirstOrDefault(c => c.Id ==Id);

        if (existingClient == null) throw new Exception($"Клиент не найден.");

        var accountToRemove = existingClient.Accounts.FirstOrDefault(a => a.CurrencyId == currencyId);
        if (accountToRemove == null) throw new Exception("Счёт не найден.");

        existingClient.Accounts.Remove(accountToRemove);
        _context.SaveChanges();

        return true;
    }

    public List<Account> GetAccountsByClient(Client client)
    {
        var existingClient = _context.Clients
            .Include(c => c.Accounts)
            .ThenInclude(a => a.Currency)
            .FirstOrDefault(c => c.Id == client.Id);

        if (existingClient == null) throw new Exception($"Клиент {client.Name} не найден.");

        return existingClient.Accounts;
    }

    public Client GetById(Guid id)
    {
        return _context.Clients.FirstOrDefault(c=>c.Id==id) ?? throw new Exception($"Клиент не найден.");;
    }
}