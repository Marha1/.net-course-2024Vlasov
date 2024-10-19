using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Implementations;

public class EmployeeStorage: BaseStorage<Employee>, IEmployeeStorage
{
    private readonly BankSystemDbContext _context;

    public EmployeeStorage(BankSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
}