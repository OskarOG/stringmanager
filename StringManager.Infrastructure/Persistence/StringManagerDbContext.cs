using Microsoft.EntityFrameworkCore;
using StringManager.Application.Persistence;
using StringManager.Infrastructure.Persistence.Configuration;

namespace StringManager.Infrastructure.Persistence;

public class StringManagerDbContext : DbContext, IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public StringManagerDbContext(DbContextOptions<StringManagerDbContext> options)
        : base(options)
    {
    }
    
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return _repositories[typeof(TEntity)] as IRepository<TEntity>
                ?? throw new NullReferenceException(
                    $"Repository for type {typeof(TEntity).Name} is null at resolve");
        }

        var newRepo = new Repository<TEntity>(this);
        _repositories.Add(typeof(TEntity), newRepo);

        return newRepo;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            // .UseSqlServer(_connectionString)
            .UseLazyLoadingProxies();
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccessGroupEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FolderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}