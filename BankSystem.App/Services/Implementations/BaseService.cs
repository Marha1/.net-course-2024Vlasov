using BankSystem.App.Services.Interfaces;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;
using BankSystem.App.Exceptions;

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
        if (entity == null) 
            throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

        if (entity.Age < 18)
            throw new AgeException("Возраст должен быть не менее 18 лет.");

        if (string.IsNullOrWhiteSpace(entity.PassportDetails))
            throw new PassportException("Паспортные данные отсутствуют.");

        _storage.Add(entity);
    }

    public bool Update(T entity)
    {
        try
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

            if (entity.Age < 18)
                throw new AgeException("Возраст должен быть не менее 18 лет.");

            if (string.IsNullOrWhiteSpace(entity.PassportDetails))
                throw new PassportException("Паспортные данные отсутствуют.");

            return _storage.Update(entity);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(T entity)
    {
        try
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

            return _storage.Delete(entity);
        }
        catch
        {
            return false;
        }
    }

    public IReadOnlyList<T> GetEntities(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> filter = null)
    {
        try
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Номер страницы должен быть больше 0.");

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Размер страницы должен быть больше 0.");

            return _storage.GetEntities(pageNumber, pageSize, filter);
        }
        catch
        {
            return null;
        }
    }
}
