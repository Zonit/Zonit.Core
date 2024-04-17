namespace Zonit.Extensions.Databases.Abstractions.Repositories;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T1">Model name</typeparam>
/// <typeparam name="T2">ID Type</typeparam>
public interface IBaseRepository<T1, T2>
{
    Task<T2> AddAsync(T1 entity);
    Task<T1?> GetByIdAsync(T2 id);
    Task<bool> UpdateAsync(T1 entity);
    Task<bool> DeleteAsync(T2 id);
}
