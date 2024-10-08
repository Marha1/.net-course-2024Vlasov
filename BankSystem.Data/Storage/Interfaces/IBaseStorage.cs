using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Interfaces;

public interface IBaseStorage<T> where T: Person
{
    void Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    
}