namespace BankSystemDomain.Models;

public class Currency
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Account> Accounts { get; set; } = new List<Account>();
}