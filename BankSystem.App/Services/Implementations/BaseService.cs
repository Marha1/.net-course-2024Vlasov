using BankSystem.App.Exceptions;
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

    public virtual async Task AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

        if (string.IsNullOrWhiteSpace(entity.PassportDetails))
            throw new PassportException("Паспортные данные отсутствуют.");

        await _storage.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

        if (entity.Age < 18)
            throw new AgeException("Возраст должен быть не менее 18 лет.");

        if (string.IsNullOrWhiteSpace(entity.PassportDetails))
            throw new PassportException("Паспортные данные отсутствуют.");

        return await _storage.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

        return await _storage.DeleteAsync(entity);
    }

    public async Task<IReadOnlyList<T>> GetEntitiesAsync(int pageNumber, int pageSize,
        Func<IQueryable<T>, IQueryable<T>> filter = null)
    {
        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Номер страницы должен быть больше 0.");

        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Размер страницы должен быть больше 0.");

        return await _storage.GetEntitiesAsync(pageNumber, pageSize, filter);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _storage.GetByIdAsync(id);
    }
}