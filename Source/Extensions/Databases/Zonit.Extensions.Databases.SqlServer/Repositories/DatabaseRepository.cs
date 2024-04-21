using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Zonit.Extensions.Databases.Abstractions.Exceptions;
using Zonit.Extensions.Databases.Abstractions.Repositories;
using Zonit.Extensions.Databases.SqlServer.Services;

namespace Zonit.Extensions.Databases.SqlServer.Repositories;

// TODO: Pomyśl by wywalić ID, zrobić na predicate

public abstract class DatabaseRepository<TEntity, TType>(DbContext _context) : IDatabaseRepository<TEntity, TType> 
    where TEntity : class
{
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity is null)
            throw new DatabaseException($"The {nameof(entity)} parameter cannot be null.");

        await _context.Set<TEntity>().AddAsync(entity);

        if (await _context.SaveChangesAsync() > 0 is false)
            throw new DatabaseException("There was a problem when creating record.");

        return entity;
    }

    public async Task<TEntity?> GetAsync(TType id)
    {
        var property = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

        if (property is null || property.PropertyType != typeof(TType))
            throw new DatabaseException($"Entity's Id property not found or type mismatched with {typeof(TType).Name}.");

        var entity = await _context
            .Set<TEntity>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => ((TType)property.GetValue(x)!).Equals(id));

        return entity;
    }

    public async Task<TDto?> GetAsync<TDto>(TType id)
        => MappingService.Dto<TDto?>(await this.GetAsync(id));

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context
            .Set<TEntity>()
            .AsNoTracking()
            .SingleOrDefaultAsync(predicate);

    public async Task<TDto?> GetAsync<TDto>(Expression<Func<TEntity, bool>> predicate)
        => MappingService.Dto<TDto?>(await this.GetAsync(predicate));

    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context
            .Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);

    public async Task<TDto?> GetFirstAsync<TDto>(Expression<Func<TEntity, bool>> predicate)
        => MappingService.Dto<TDto?>(await this.GetFirstAsync(predicate));

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var property = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

        if (property is null || property.PropertyType != typeof(TType))
            throw new DatabaseException($"Entity's Id property not found or type mismatched with {typeof(TType).Name}.");

        var id = (TType)property.GetValue(entity)!;

        var existingEntity = await _context.Set<TEntity>().FindAsync(id);

        if (existingEntity is null)
            return false;

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        //_context.Set<T1>().Update(entity);

        if (await _context.SaveChangesAsync() > 0 is false)
            throw new DatabaseException("There was a problem when updating record.");

        return true;
    }

    public async Task<bool> DeleteAsync(TType id)
    {
        var entity = await _context
            .Set<TEntity>()
            .FindAsync(id);

        if (entity is null)
            return false;

        _context
            .Set<TEntity>()
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