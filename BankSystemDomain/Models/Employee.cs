using System;

namespace BankSystemDomain.Models;

public class Employee:Person
{
    public int Expirence { get; set; }
    public decimal Salary { get; set; }
    public string Contract { get; set; }
    public override bool Equals(object? obj) 
    { 
        if (obj is not Employee employee) 
            return false; 

        return Name == employee.Name 
               && Surname == employee.Surname 
               && Age == employee.Age 
               && PhoneNumber == employee.PhoneNumber 
               && Expirence == employee.Expirence 
               && Salary == employee.Salary; 
    } 

    public override int GetHashCode() 
    { 
        return HashCode.Combine(Name, Surname, Age, PhoneNumber, Expirence, Salary); 
    } 
}