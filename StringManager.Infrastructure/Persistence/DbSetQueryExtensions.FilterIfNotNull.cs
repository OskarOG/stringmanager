using System.Linq.Expressions;

namespace StringManager.Infrastructure.Persistence;

public static partial class DbSetQueryExtensions
{
    public static IQueryable<TEntity> FilterIfNotNull<TEntity>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? filter) =>
        filter != null
            ? query.Where(filter)
            : query;
}