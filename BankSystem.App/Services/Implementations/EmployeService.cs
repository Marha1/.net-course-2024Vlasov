using BankSystem.App.Services.Interfaces;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.App.Services.Implementations;

public class EmployeService : BaseService<Employee>, IEmployeeService
{
    public EmployeService(IBaseStorage<Employee> storage) : base(storage)
    {
    }
}