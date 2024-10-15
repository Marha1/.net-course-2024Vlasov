using BankSystem.App.Services.Interfaces;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.App.Services.Implementations;

public class EmployeService : BaseService<Employee>, IEmployeeService
{
    private readonly IEmployeeStorage _employStorage;
    public EmployeService(IEmployeeStorage employeeStorage) : base(employeeStorage)
    {
        _employStorage = employeeStorage;
    }
    public Employee GetById(Guid id)
    {
        
       return _employStorage.GetById(id);
    }
}