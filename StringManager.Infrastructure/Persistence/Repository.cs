using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StringManager.Application.Persistence;

namespace StringManager.Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly StringManagerDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(StringManagerDbContext stringManagerDbContext)
    {
        _dbContext = stringManagerDbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public void Delete(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public Task<ICollection<TEntity>> GetAsync(params string[] includeProperties) =>
        GetAsync(null, null, includeProperties);

    public Task<ICollection<TEntity>> GetAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        params string[] includeProperties) =>
        GetAsync(null, orderBy, includeProperties);

    public Task<ICollection<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        params string[] includeProperties) =>
        GetAsync(filter, null, includeProperties);

    public Task<ICollection<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        params string[] includeProperties) =>
        _dbSet
            .AsQueryable()
            .FilterIfNotNull(filter)
            .IncludeReferences(includeProperties)
            .ToOrderedListAsync(orderBy);

    public async Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> filter,
        params string[] includeProperties) =>
        await _dbSet
            .Where(filter)
            .SingleOrDefaultAsync()
        ?? throw new RepositoryException("No single entity was found for the used filter.");

    public TEntity Insert(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Added;
        return entity;
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}