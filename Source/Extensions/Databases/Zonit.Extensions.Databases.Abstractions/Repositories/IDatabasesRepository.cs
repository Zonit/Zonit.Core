using System.Linq.Expressions;

namespace Zonit.Extensions.Databases.Abstractions.Repositories;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Model name</typeparam>
public interface IDatabasesRepository<T> : IDisposable
{
    IDatabasesRepository<T> Skip(int skip);
    IDatabasesRepository<T> Take(int take);
    IDatabasesRepository<T> Include(Expression<Func<T, object>> includeExpression);
    IDatabasesRepository<T> Where(Expression<Func<T, bool>> predicate);
    IDatabasesRepository<T> OrderBy(Expression<Func<T, object>> keySelector);
    IDatabasesRepository<T> OrderByDescending(Expression<Func<T, object>> keySelector);
    IDatabasesRepository<T> Select(Expression<Func<T, T>> selector);
    Task<IReadOnlyCollection<T>> GetAsync();
    Task<int> GetCountAsync();
}
