using BankSystemDomain.Models;

namespace BankSystem.App.Services.Storage;

public class ClientStorage
{
    private readonly List<Client> _clients = new();

    public void AddClient(Client client)
    {
        _clients.Add(client);
    }
    
    public IReadOnlyList<Client> GetClients()
    {
        return _clients.AsReadOnly();
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
    public List<Client> GetClientsByFilter(string? name, string? phoneNumber, string? passportDetails,
        DateTime? birthDateFrom, DateTime? birthDateTo)
    {
        return _clients.Where(client =>
            (string.IsNullOrEmpty(name) || client.Name.Contains(name) || client.Surname.Contains(name)) &&
            (string.IsNullOrEmpty(phoneNumber) || client.PhoneNumber == phoneNumber) &&
            (string.IsNullOrEmpty(passportDetails) || client.PassportDetails == passportDetails) &&
            (!birthDateFrom.HasValue || client.BirthDate >= birthDateFrom) &&
            (!birthDateTo.HasValue || client.BirthDate <= birthDateTo)
        ).ToList();
    }

    public Client GetYoungestClient()
    {
        return _clients.OrderByDescending(client => client.BirthDate).FirstOrDefault();
    }

    public Client GetOldestClient()
    {
        return _clients.OrderBy(client => client.BirthDate).FirstOrDefault();
    }

    public double CalculateAverageAge()
    {
        return _clients.Average(client => client.Age);
    }
}