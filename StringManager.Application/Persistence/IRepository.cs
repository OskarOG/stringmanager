using System.Linq.Expressions;

namespace StringManager.Application.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    void Delete(TEntity entity);

    Task<ICollection<TEntity>> GetAsync(params string[] includeProperties);
    
    Task<ICollection<TEntity>> GetAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        params string[] includeProperties);
    
    Task<ICollection<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        params string[] includeProperties);
    
    Task<ICollection<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        params string[] includeProperties);
    
    // TODO: Create a way to use expression for include properties
    // params Expression<Func<TEntity, TProperty>>[] includeProperties 

    Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> filter,
        params string[] includeProperties);
    
    TEntity Insert(TEntity entity);

    void Update(TEntity entity);
}