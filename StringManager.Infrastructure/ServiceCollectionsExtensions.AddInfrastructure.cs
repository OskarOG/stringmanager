using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StringManager.Application.Persistence;
using StringManager.Infrastructure.Persistence;

namespace StringManager.Infrastructure;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service) =>
        service.AddDbContext<IUnitOfWork, StringManagerDbContext>(options =>
            options.UseSqlServer("name=ConnectionStrings:StringManagerDb"));
}