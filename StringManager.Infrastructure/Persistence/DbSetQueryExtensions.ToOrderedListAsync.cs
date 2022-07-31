using Microsoft.EntityFrameworkCore;

namespace StringManager.Infrastructure.Persistence;

public static partial class DbSetQueryExtensions
{
    public static async Task<ICollection<TEntity>> ToOrderedListAsync<TEntity>(
        this IQueryable<TEntity> query,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy) =>
        orderBy != null
            ? await orderBy(query).ToListAsync() // TODO: Can this be closed down more to only allow the order setting
            : await query.ToListAsync();
}