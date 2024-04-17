using System.Linq.Expressions;

namespace Zonit.Extensions.Databases.Abstractions.Repositories;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Model name</typeparam>
public interface IBasesRepository<T>
{
    IBasesRepository<T> Skip(int skip);
    IBasesRepository<T> Take(int take);
    IBasesRepository<T> Include(Expression<Func<T, object>> includeExpression);
    IBasesRepository<T> Where(Expression<Func<T, bool>> predicate);
    IBasesRepository<T> OrderBy(Expression<Func<T, object>> keySelector);
    IBasesRepository<T> OrderByDescending(Expression<Func<T, object>> keySelector);
    IBasesRepository<T> Select(Expression<Func<T, T>> selector);
    Task<IReadOnlyCollection<T>> GetAsync();
    Task<int> GetCountAsync();
}
