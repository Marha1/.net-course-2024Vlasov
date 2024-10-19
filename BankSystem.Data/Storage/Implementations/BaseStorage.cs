using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Implementations;

public abstract class BaseStorage<T> : IBaseStorage<T> where T : Person
{
    private readonly BankSystemDbContext _context;

    protected BaseStorage(BankSystemDbContext context)
    {
        _context = context;
    }

    public virtual  void Add(T entity)
    {
        if (_context.Set<T>().Any(e => e.Equals(entity)))
            throw new Exception($"{typeof(T).Name} уже существует.");

        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public virtual  bool Update(T entity)
    {
        var existingEntity = _context.Set<T>().Find(entity.Id);
        if (existingEntity == null) throw new Exception($"{typeof(T).Name} не найден.");

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        _context.SaveChanges();

        return true;
    }

    public virtual  bool Delete(T entity)
    {
        var existingEntity = _context.Set<T>().Find(entity.Id);
        if (existingEntity == null) throw new Exception($"{typeof(T).Name} не найден.");

        _context.Set<T>().Remove(existingEntity);
        _context.SaveChanges();

        return true;
    }

    public virtual  IReadOnlyList<T> GetEntities(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> filter = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null) query = filter(query);

        var result = query
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return result.AsReadOnly();
    }

    public T GetById(Guid id)
    {
        return _context.Set<T>().FirstOrDefault(c => c.Id == id) ?? throw new Exception($"Сущность с Id {id} не найдена.");
    }
}