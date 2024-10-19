using System;

namespace BankSystemDomain.Models;

public class Employee:Person
{
    public int Expirence { get; set; }
    public decimal Salary { get; set; }
    public string Contract { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not Employee client)
            return false;

        return Id == client.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

}