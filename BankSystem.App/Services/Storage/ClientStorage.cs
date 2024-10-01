using BankSystemDomain.Models;

namespace BankSystem.App.Services.Storage;

public class ClientStorage
{
    private readonly List<Client> _clients = new();

    public void AddClient(Client client)
    {
        _clients.Add(client);
    }

    public List<Client> GetClients()
    {
        return _clients;
    }

    public bool RemoveClient(string phoneNumber)
    {
        var clientToRemove = _clients.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (clientToRemove != null)
        {
            _clients.Remove(clientToRemove);
            return true;
        }

        return false;
    }

    public bool UpdateClient(string phoneNumber, Client updatedClient)
    {
        var clientToUpdate = _clients.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (clientToUpdate != null)
        {
            clientToUpdate.Name = updatedClient.Name;
            clientToUpdate.Surname = updatedClient.Surname;
            clientToUpdate.PassportDetails = updatedClient.PassportDetails;
            clientToUpdate.Age = updatedClient.Age;
            clientToUpdate.BirthDate = updatedClient.BirthDate;
            return true;
        }

        return false;
    }
}