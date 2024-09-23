namespace BankSystemDomain.Models;

public class Employee:Person
{
    public int Expirence { get; set; }
    public decimal Salary { get; set; }
    public string Contract { get; set; }
}