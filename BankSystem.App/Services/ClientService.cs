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

    public void AddClient(Client client)
    {
        _clientStorage.AddClient(client);
    }


    public bool RemoveClient(string phoneNumber)
    {
        return _clientStorage.RemoveClient(phoneNumber);
    }

    public bool UpdateClient(string phoneNumber, Client updatedClient)
    {
        return _clientStorage.UpdateClient(phoneNumber, updatedClient);
    }

    public void AddAccount(string phoneNumber, Account newAccount)
    {
        _clientStorage.AddAccount(phoneNumber, newAccount);
    }

    public bool UpdateClientAccount(string phoneNumber, Account updatedAccount)
    {
        return _clientStorage.UpdateClientAccount(phoneNumber, updatedAccount);
    }

    public List<Client> GetClientsByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return _clientStorage.GetClientsByFilter(name, phoneNumber, passportDetails, birthDateFrom, birthDateTo);
    }

    public Client GetOldestClient()
    {
        return _clientStorage.GetOldestClient();
    }

    public Client GetYoungestClient()
    {
        return _clientStorage.GetYoungestClient();
    }

    public double CalculateAverageAge()
    {
        var clients = _clientStorage.GetClients();
        return clients.Count > 0
            ? clients.Average(client => client.Key.Age)
            : 0;
    }
}