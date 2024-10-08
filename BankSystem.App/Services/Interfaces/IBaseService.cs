using BankSystemDomain.Models;

namespace BankSystem.App.Services.Interfaces;

public interface IBaseService<T> where T : Person
{
    void Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    IReadOnlyList<T> GetEntities(int pageNumber, int pageSize);


}