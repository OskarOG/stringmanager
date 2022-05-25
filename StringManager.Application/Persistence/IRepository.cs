using System.Linq.Expressions;

namespace StringManager.Application.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    void Delete(TEntity entity);

    Task<IEnumerable<TEntity>> GetAsync(params string[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        params string[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        params string[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        params string[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAsync<TProperty>(params Expression<Func<TEntity, TProperty>>[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAsync<TProperty>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        params Expression<Func<TEntity, TProperty>>[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAsync<TProperty>(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, bool>>[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAsync<TProperty>(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        params Expression<Func<TEntity, TProperty>>[] includeProperties);

    TEntity Insert(TEntity entity);

    void Update(TEntity entity);
}