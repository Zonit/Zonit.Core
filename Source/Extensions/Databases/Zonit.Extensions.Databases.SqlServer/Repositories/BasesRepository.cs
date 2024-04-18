using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zonit.Extensions.Databases.Abstractions.Repositories;

namespace Zonit.Extensions.Databases.SqlServer.Repositories;

public abstract class BasesRepository<T, TContext>(IDbContextFactory<TContext> _context) : IBasesRepository<T> 
    where T : class
    where TContext : DbContext
{
    List<Expression<Func<T, object>>>? IncludeExpressions { get; set; }
    Expression<Func<T, bool>>? FilterExpression { get; set; }
    Expression<Func<T, object>>? OrderByColumnSelector { get; set; }
    Expression<Func<T, object>>? OrderByDescendingColumnSelector { get; set; }
    Expression<Func<T, T>>? SelectColumns { get; set; }
    int? SkipCount { get; set; }
    int? TakeCount { get; set; }

    public async Task<IReadOnlyCollection<T>> GetAsync()
    {
        var context = await _context.CreateDbContextAsync();

        var entitie = context.Set<T>()
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

        return await entitie.ToListAsync().ConfigureAwait(false);
    }

    public async Task<int> GetCountAsync()
    {
        var context = await _context.CreateDbContextAsync();

        var entitie = context.Set<T>()
            .AsNoTracking();

        if (FilterExpression is not null)
            entitie = entitie.Where(FilterExpression);

        return await entitie.CountAsync().ConfigureAwait(false);
    }

    public IBasesRepository<T> Include(Expression<Func<T, object>> includeExpression)
    {
        IncludeExpressions ??= [];
        IncludeExpressions.Add(includeExpression);
        return this;
    }

    public IBasesRepository<T> OrderBy(Expression<Func<T, object>> keySelector)
    {
        OrderByColumnSelector = keySelector;
        return this;
    }

    public IBasesRepository<T> OrderByDescending(Expression<Func<T, object>> keySelector)
    {
        OrderByDescendingColumnSelector = keySelector;
        return this;
    }

    public IBasesRepository<T> Select(Expression<Func<T, T>> selector)
    {
        SelectColumns = selector;
        return this;
    }

    public IBasesRepository<T> Skip(int skip)
    {
        SkipCount = skip;
        return this;
    }

    public IBasesRepository<T> Take(int take)
    {
        TakeCount = take;
        return this;
    }

    public IBasesRepository<T> Where(Expression<Func<T, bool>> predicate)
    {
        if (FilterExpression is null)
            FilterExpression = predicate;
        else
        {
            var invokedExpr = Expression.Invoke(predicate, FilterExpression.Parameters.Cast<Expression>());
            FilterExpression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(FilterExpression.Body, invokedExpr), FilterExpression.Parameters);
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
