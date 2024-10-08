using BankSystem.App.Services.Interfaces;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.App.Services.Implementations;

public class BaseService<T> : IBaseService<T> where T : Person
{
    private readonly IBaseStorage<T> _storage;

    public BaseService(IBaseStorage<T> storage)
    {
        _storage = storage;
    }

    public void Add(T entity)
    {
        _storage.Add(entity);
    }

    public bool Update(T entity)
    {
        return _storage.Update(entity);
    }

    public bool Delete(T entity)
    {
        return _storage.Delete(entity);
    }

    public IReadOnlyList<T> GetEntities(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Номер страницы должен быть больше 0.");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Размер страницы должен быть больше 0.");
        }

        return _storage.GetEntities(pageNumber, pageSize);
    }
}
