using BankSystem.App.Services.Storage;
using BankSystemDomain.Models;

namespace BankSystem.App.Services;

public class ClientService
{
    private readonly ClientStorage _clientStorage;

    public ClientService(ClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }

    public List<Client> GetClientsByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return _clientStorage.GetClientsByFilter(name, phoneNumber, passportDetails, birthDateFrom, birthDateTo);
    }
}