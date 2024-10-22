using BankSystemDomain.Models;

namespace BankSystem.App.Services.Interfaces;

public interface IBaseService<T> 
{
    void Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    IReadOnlyList<T> GetEntities(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> filter = null);
    T GetById(Guid Id);


}