using StringManager.Application.Services.Domain;

namespace StringManager.API.Configuration;

internal static partial class ServiceCollectionExtensions
{
    internal static IServiceCollection AddDomainServices(this IServiceCollection services) =>
        services
            .AddTransient<IProblemDetailFactory, ProblemDetailFactory>()
            .AddTransient<ITokenCreationService, TokenCreationService>();
}