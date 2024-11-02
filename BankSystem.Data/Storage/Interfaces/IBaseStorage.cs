namespace BankSystem.Data.Storage.Interfaces;

public interface IBaseStorage<T>
{
    Task AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);

    Task<IReadOnlyList<T>> GetEntitiesAsync(int pageNumber, int pageSize,
        Func<IQueryable<T>, IQueryable<T>> filter = null);

    Task<T> GetByIdAsync(Guid id);
}