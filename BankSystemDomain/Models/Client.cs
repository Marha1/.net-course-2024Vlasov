namespace BankSystemDomain.Models 
{ 
    public class Client : Person 
    { 
        private bool _overrideEqualsAndHashCode; 

        public Client(bool overrideEqualsAndHashCode) 
        { 
            _overrideEqualsAndHashCode = overrideEqualsAndHashCode; 
        } 

        public override bool Equals(object? obj) 
        { 
            if (obj is not Client client)
                return false;

            if (!_overrideEqualsAndHashCode) 
                return base.Equals(obj); 
            
            return Name == client.Name && Surname == client.Surname && Age == client.Age && PhoneNumber == client.PhoneNumber; 
        } 

        public override int GetHashCode() 
        { 
            if (!_overrideEqualsAndHashCode) return base.GetHashCode();
            return HashCode.Combine(Name, Surname, Age, PhoneNumber);
        } 
    } 
}