using System.Linq.Expressions;

namespace Zonit.Extensions.Databases.Abstractions.Repositories;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEntity">Model name</typeparam>
/// <typeparam name="TType">ID Type</typeparam>
public interface IDatabaseRepository<TEntity, TType>
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> GetAsync(TType id);
    Task<TDto?> GetAsync<TDto>(TType id);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TDto?> GetAsync<TDto>(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TDto?> GetFirstAsync<TDto>(Expression<Func<TEntity, bool>> predicate);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TType entity);
}