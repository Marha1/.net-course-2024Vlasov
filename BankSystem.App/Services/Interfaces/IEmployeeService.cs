using BankSystemDomain.Models;

namespace BankSystem.App.Services.Interfaces;

public interface IEmployeeService: IBaseService<Employee>
{
    public Employee GetById(Guid id);
}