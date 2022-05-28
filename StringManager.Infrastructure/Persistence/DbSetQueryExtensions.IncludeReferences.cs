using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace StringManager.Infrastructure.Persistence;

public static partial class DbSetQueryExtensions
{
    public static IQueryable<TEntity> IncludeReferences<TEntity>(
        this IQueryable<TEntity> query,
        IEnumerable<string> includes)
        where TEntity : class
    {
        foreach (var include in includes)
        {
            query.Include(include);
        }

        return query;
    }

    // TODO: Create a way to use ThenInclude as well
    public static IQueryable<TEntity> IncludeReferences<TEntity, TProperty>(
        this IQueryable<TEntity> query,
        IEnumerable<Expression<Func<TEntity, TProperty>>> properties)
        where TEntity : class
    {
        foreach (var property in properties)
        {
            query.Include(property);
        }

        return query;
    }
}