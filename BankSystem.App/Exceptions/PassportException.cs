namespace BankSystem.App.Exceptions;

public class PassportException : Exception
{
    public PassportException()
        : base("Паспортные данные отсутствуют.")
    {
    }

    public PassportException(string message)
        : base(message)
    {
    }

    public PassportException(string message, Exception inner)
        : base(message, inner)
    {
    }
}