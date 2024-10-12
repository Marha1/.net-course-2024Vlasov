using System;

namespace BankSystemDomain.Models;

public class Person
{
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string  PassportDetails { get; set; }
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
    
}