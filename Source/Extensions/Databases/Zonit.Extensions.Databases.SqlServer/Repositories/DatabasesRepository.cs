using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zonit.Extensions.Databases.Abstractions.Exceptions;
using Zonit.Extensions.Databases.Abstractions.Repositories;
using Zonit.Extensions.Databases.SqlServer.Services;

namespace Zonit.Extensions.Databases.SqlServer.Repositories;

public abstract class DatabasesRepository<TEntity, TContext>(IDbContextFactory<TContext> _context) : IDatabasesRepository<TEntity> 
    where TEntity : class
    where TContext : DbContext
{
    List<Expression<Func<TEntity, object>>>? IncludeExpressions { get; set; }
    Expression<Func<TEntity, bool>>? FilterExpression { get; set; }
    Expression<Func<TEntity, object>>? OrderByColumnSelector { get; set; }
    Expression<Func<TEntity, object>>? OrderByDescendingColumnSelector { get; set; }
    Expression<Func<TEntity, TEntity>>? SelectColumns { get; set; }
    int? SkipCount { get; set; }
    int? TakeCount { get; set; }

    public async Task<IReadOnlyCollection<TEntity>?> GetAsync(CancellationToken cancellationToken = default)
    {
        var context = await _context.CreateDbContextAsync();

        var entitie = context.Set<TEntity>()
            .AsSplitQuery()
            .AsNoTracking();

        entitie = FilterExpression is not null ? entitie.Where(FilterExpression) : entitie;
        entitie = OrderByColumnSelector is not null ? entitie.OrderBy(OrderByColumnSelector) : entitie;
        entitie = OrderByDescendingColumnSelector is not null ? entitie.OrderByDescending(OrderByDescendingColumnSelector) : entitie;
        entitie = SelectColumns is not null ? entitie.Select(SelectColumns) : entitie;

        if (IncludeExpressions is not null)
            foreach (var includeExpression in IncludeExpressions)
                entitie = entitie.Include(includeExpression);

        entitie = SkipCount is not null ? entitie.Skip(SkipCount.Value) : entitie;
        entitie = TakeCount is not null ? entitie.Take(TakeCount.Value) : entitie;

        var result = await entitie.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (result is null || result.Count == 0)
            return null;

        return result;
    }

    public async Task<IReadOnlyCollection<TDto>?> GetAsync<TDto>(CancellationToken cancellationToken = default)
        => MappingService.Dto<TDto>(await this.GetAsync(cancellationToken));
    
    public async Task<int> GetCountAsync()
    {
        var context = await _context.CreateDbContextAsync();

        var entitie = context.Set<TEntity>()
            .AsNoTracking();

        if (FilterExpression is not null)
            entitie = entitie.Where(FilterExpression);

        return await entitie.CountAsync().ConfigureAwait(false);
    }

    public IDatabasesRepository<TEntity> Include(Expression<Func<TEntity, object>> includeExpression)
    {
        IncludeExpressions ??= [];
        IncludeExpressions.Add(includeExpression);
        return this;
    }

    public IDatabasesRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector)
    {
        OrderByColumnSelector = keySelector;
        return this;
    }

    public IDatabasesRepository<TEntity> OrderByDescending(Expression<Func<TEntity, object>> keySelector)
    {
        OrderByDescendingColumnSelector = keySelector;
        return this;
    }

    public IDatabasesRepository<TEntity> Select(Expression<Func<TEntity, TEntity>> selector)
    {
        SelectColumns = selector;
        return this;
    }

    public IDatabasesRepository<TEntity> Skip(int skip)
    {
        SkipCount = skip;
        return this;
    }

    public IDatabasesRepository<TEntity> Take(int take)
    {
        TakeCount = take;
        return this;
    }

    public IDatabasesRepository<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        if (FilterExpression is null)
            FilterExpression = predicate;
        else
        {
            var invokedExpr = Expression.Invoke(predicate, FilterExpression.Parameters.Cast<Expression>());
            FilterExpression = Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(FilterExpression.Body, invokedExpr), FilterExpression.Parameters);
        }
        return this;
    }

    public void Dispose()
    {
        IncludeExpressions = null;
        FilterExpression = null;
        OrderByColumnSelector = null;
        OrderByDescendingColumnSelector = null;
        SelectColumns = null;
        SkipCount = null;
        TakeCount = null;
    }
}
