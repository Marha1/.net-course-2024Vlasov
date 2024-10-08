using BankSystem.App.Exceptions;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.Data.Storage.Implementations;

public abstract class BaseStorage<T> : IBaseStorage<T> where T: Person
{
    private readonly List<T> _items; 

    internal BaseStorage()
    {
        _items = new List<T>();
    }
    public void Add(T entity)
    {
        ValidateEntity(entity); 

        if (_items.Contains(entity))
        {
            throw new Exception($"{typeof(T).Name} уже существует.");
        }
        
        _items.Add(entity);
    }

    public bool Update(T entity)
    {
        ValidateEntity(entity);

        var itemToUpdate = _items.FirstOrDefault(entity);
        if (itemToUpdate == null)
        {
            throw new Exception($"{typeof(T).Name} не найден.");
        }

        _items.Remove(itemToUpdate);
        _items.Add(entity); 

        return  true;
    }

    public bool Delete(T entity)
    {
        var itemToRemove = _items.FirstOrDefault(entity);
        if (itemToRemove == null)
        {
            throw new Exception($"{typeof(T).Name} не найден.");
        }

        _items.Remove(itemToRemove);
        return true;
    }

    public IReadOnlyList<T> GetEntities(int pageNumber, int pageSize)
    {
       
        return _items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList()
            .AsReadOnly();
    }

    protected virtual void ValidateEntity(T entity)
    {
        if (entity.Age < 18)
        {
            throw new AgeException("Моложе 18 лет!");
        }

        if (entity is Client client && string.IsNullOrEmpty(client.PassportDetails)||entity is Employee employ && string.IsNullOrEmpty(employ.PassportDetails))
        {
            throw new PassportException("Паспортные данные отсутствуют.");
        }
    }
}