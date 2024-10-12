using System;

namespace BankSystem.App.Exceptions
{
    public class AgeException : Exception
    {
        public AgeException()
            : base("Моложе 18 лет!")
        {
        }

        public AgeException(string message)
            : base(message)
        {
        }

        public AgeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    
}