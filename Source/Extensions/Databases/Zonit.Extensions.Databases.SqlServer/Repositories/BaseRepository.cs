using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;
using Zonit.Extensions.Databases.Abstractions.Repositories;

namespace Zonit.Extensions.Databases.SqlServer.Repositories;

public abstract class BaseRepository<T1, T2> : IBaseRepository<T1, T2> where T1 : class
{
    readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<T2> AddAsync(T1 entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T1?> GetByIdAsync(T2 id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(T1 entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(T2 id)
    {
        throw new NotImplementedException();
    }
}