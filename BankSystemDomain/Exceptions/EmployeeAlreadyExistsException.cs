using System;

namespace BankSystem.App.Exceptions;

public class EmployeeAlreadyExistsException : Exception
{
    public EmployeeAlreadyExistsException()
        : base("Сотрудник уже существует.")
    {
    }

    public EmployeeAlreadyExistsException(string message)
        : base(message)
    {
    }

    public EmployeeAlreadyExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}