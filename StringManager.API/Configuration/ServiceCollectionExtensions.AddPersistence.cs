using Microsoft.EntityFrameworkCore;
using StringManager.Application.Persistence;
using StringManager.Infrastructure.Persistence;

namespace StringManager.API.Configuration;

internal static partial class ServiceCollectionExtensions
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services) =>
        services.AddDbContext<IUnitOfWork, StringManagerDbContext>(options =>
            options.UseSqlServer("name=ConnectionStrings:StringManagerDb"));
}