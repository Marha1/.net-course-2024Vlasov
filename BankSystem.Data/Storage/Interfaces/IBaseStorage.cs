using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Interfaces;

public interface IBaseStorage<T> 
{
    void Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    IReadOnlyList<T> GetEntities(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> filter = null);



}