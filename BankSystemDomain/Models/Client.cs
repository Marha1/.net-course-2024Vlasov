namespace BankSystemDomain.Models;

public class Client : Person
{
    public List<Account> Accounts { get; set; } = new();

    public override bool Equals(object? obj)
    {
        if (obj is not Client client)
            return false;

        return Id == client.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

}