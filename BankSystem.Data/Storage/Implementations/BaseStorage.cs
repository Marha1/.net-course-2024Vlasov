using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storage.Implementations;

public abstract class BaseStorage<T> : IBaseStorage<T> where T : Person
{
    private readonly BankSystemDbContext _context;

    protected BaseStorage(BankSystemDbContext context)
    {
        _context = context;
    }

    public virtual async Task AddAsync(T entity)
    {
        if (await _context.Set<T>().AnyAsync(e => e.Equals(entity)))
            throw new Exception($"{typeof(T).Name} уже существует.");

        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(entity.Id);
        if (existingEntity == null) throw new Exception($"{typeof(T).Name} не найден.");

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(entity.Id);
        if (existingEntity == null) throw new Exception($"{typeof(T).Name} не найден.");

        _context.Set<T>().Remove(existingEntity);
        await _context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<IReadOnlyList<T>> GetEntitiesAsync(int pageNumber, int pageSize,
        Func<IQueryable<T>, IQueryable<T>> filter = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null) query = filter(query);

        return await query
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(c => c.Id == id) ??
               throw new Exception($"Сущность с Id {id} не найдена.");
    }
}