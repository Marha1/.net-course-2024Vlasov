using BankSystemDomain.Models;

namespace BankSystem.App.Services.Interfaces;

public interface IClientService : IBaseService<Client>
{
    List<Client> GetClientsByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo, int pageNumber, int pageSize);
        
    void AddAccountToClient(string phoneNumber, Account account);
    bool UpdateClientAccount(string phoneNumber, Account account);
    bool DeleteClientAccount(string phoneNumber, string currency);
    List<Account> GetAccountsByPhoneNumber(string phoneNumber);

}