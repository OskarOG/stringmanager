namespace StringManager.Application.Persistence;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}