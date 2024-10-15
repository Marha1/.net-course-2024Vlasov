namespace BankSystemDomain.Models;

public class Person
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string  PassportDetails { get; set; }
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            if (BirthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
    public DateTime BirthDate { get; set; } 
    
}