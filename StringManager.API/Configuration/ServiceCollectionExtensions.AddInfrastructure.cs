using StringManager.Application.Services.Infrastructure;
using StringManager.Infrastructure.Services;

namespace StringManager.API.Configuration;

internal static partial class ServiceCollectionExtensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddTransient<IDateTimeService, DateTimeService>()
            .AddTransient<IProblemDetailTextService, ProblemDetailTextService>();
}