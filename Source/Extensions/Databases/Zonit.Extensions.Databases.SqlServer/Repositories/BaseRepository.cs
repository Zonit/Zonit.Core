using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Zonit.Extensions.Databases.Abstractions.Exceptions;
using Zonit.Extensions.Databases.Abstractions.Repositories;

namespace Zonit.Extensions.Databases.SqlServer.Repositories;

// TODO: Pomyśl by wywalić ID, zrobić na predicate

public abstract class BaseRepository<T1, T2>(DbContext _context) : IBaseRepository<T1, T2> where T1 : class
{
    public async Task<T1> AddAsync(T1 entity)
    {
        if (entity is null)
            throw new DatabaseException($"The {nameof(entity)} parameter cannot be null.");

        await _context.Set<T1>().AddAsync(entity);

        if (await _context.SaveChangesAsync() > 0 is false)
            throw new DatabaseException("There was a problem when creating record.");

        return entity;
    }

    public async Task<T1?> GetAsync(T2 id)
    {
        var property = typeof(T1).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

        if (property is null || property.PropertyType != typeof(T2))
            throw new DatabaseException($"Entity's Id property not found or type mismatched with {typeof(T2).Name}.");

        var entity = await _context
            .Set<T1>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => ((T2)property.GetValue(x)!).Equals(id));

        return entity;
    }

    public async Task<T1?> GetAsync(Expression<Func<T1, bool>> predicate)
    {
        var entity = await _context
            .Set<T1>()
            .AsNoTracking()
            .SingleOrDefaultAsync(predicate);

        return entity;
    }

    public async Task<T1?> GetFirstAsync(Expression<Func<T1, bool>> predicate)
    {
        var entity = await _context
            .Set<T1>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);

        return entity;
    }

    public async Task<bool> UpdateAsync(T1 entity)
    {
        var property = typeof(T1).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

        if (property is null || property.PropertyType != typeof(T2))
            throw new DatabaseException($"Entity's Id property not found or type mismatched with {typeof(T2).Name}.");

        var id = (T2)property.GetValue(entity)!;

        var existingEntity = await _context.Set<T1>().FindAsync(id);

        if (existingEntity is null)
            return false;

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        //_context.Set<T1>().Update(entity);

        if (await _context.SaveChangesAsync() > 0 is false)
            throw new DatabaseException("There was a problem when updating record.");

        return true;
    }

    public async Task<bool> DeleteAsync(T2 id)
    {
        var entity = await _context
            .Set<T1>()
            .FindAsync(id);

        if (entity is null)
            return false;

        _context
            .Set<T1>()
            .Remove(entity);

        if (await _context.SaveChangesAsync() > 0 is false)
            throw new DatabaseException("There was a problem when deleting record.");

        return true;
    }
}

//public async Task<T2> AddAsync(T1 entity)
//{
//    if (entity is null)
//        throw new DatabaseException("The {0} parameter cannot be null.", nameof(entity));

//    await _context.Set<T1>().AddAsync(entity);

//    if (await _context.SaveChangesAsync() > 0 is false)
//        throw new DatabaseException("There was a problem when creating record.");

//    var property = entity
//        .GetType()
//        .GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

//    if (property is null || property.PropertyType != typeof(T2))
//        throw new DatabaseException("Entity's Id property not found or type mismatched with {0}.", typeof(T2).Name);

//    var value = property.GetValue(entity);

//    if (value is null)
//        throw new DatabaseException("Entity's Id property value is null.");

//    return (T2)Convert.ChangeType(value, typeof(T2));
//}