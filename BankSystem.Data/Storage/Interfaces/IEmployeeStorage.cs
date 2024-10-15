using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Interfaces;

public interface IEmployeeStorage: IBaseStorage<Employee>
{
    public Employee GetById(Guid id);
}