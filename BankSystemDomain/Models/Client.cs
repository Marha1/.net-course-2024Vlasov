using System;
using System.Collections.Generic;

namespace BankSystemDomain.Models
{
    public class Client : Person
    {
        public List<Account> Accounts { get; set; } = new();

        public override bool Equals(object? obj)
        {
            if (obj is not Client client)
                return false;

            return Name == client.Name && Surname == client.Surname && Age == client.Age &&
                   PhoneNumber == client.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Age, PhoneNumber);
        }
    }
}
