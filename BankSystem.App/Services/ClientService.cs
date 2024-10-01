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
        return _clientStorage.GetClients().Where(client =>
            (string.IsNullOrEmpty(name) || client.Name.Contains(name) || client.Surname.Contains(name)) &&
            (string.IsNullOrEmpty(phoneNumber) || client.PhoneNumber == phoneNumber) &&
            (string.IsNullOrEmpty(passportDetails) || client.PassportDetails == passportDetails) &&
            (!birthDateFrom.HasValue || client.BirthDate >= birthDateFrom) &&
            (!birthDateTo.HasValue || client.BirthDate <= birthDateTo)
        ).ToList();
    }

    public Client GetYoungestClient()
    {
        return _clientStorage.GetClients().OrderByDescending(client => client.BirthDate).FirstOrDefault();
    }

    public Client GetOldestClient()
    {
        return _clientStorage.GetClients().OrderBy(client => client.BirthDate).FirstOrDefault();
    }

    public double CalculateAverageAge()
    {
        return _clientStorage.GetClients().Average(client => client.Age);
    }
}