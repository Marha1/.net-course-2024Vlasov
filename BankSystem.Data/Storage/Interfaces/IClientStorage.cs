using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Interfaces;

public interface IClientStorage: IBaseStorage<Client>
{
    public List<Client> GetByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo, int pageNumber, int pageSize);
    void AddAccount(string phoneNumber, Account newAccount);
    bool UpdateAccount(string phoneNumber, Account updatedAccount);
    bool DeleteAccount(string phoneNumber, string currency);
    List<Account> GetAccountsByPhoneNumber(string phoneNumber);

}