using System.Linq.Expressions;

namespace Zonit.Extensions.Databases.Abstractions.Repositories;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T1">Model name</typeparam>
/// <typeparam name="T2">ID Type</typeparam>
public interface IBaseRepository<T1, T2>
{
    Task<T1> AddAsync(T1 entity);
    Task<T1?> GetAsync(T2 id);
    Task<T1?> GetAsync(Expression<Func<T1, bool>> predicate);
    Task<T1?> GetFirstAsync(Expression<Func<T1, bool>> predicate);
    Task<bool> UpdateAsync(T1 entity);
    Task<bool> DeleteAsync(T2 entity);
}