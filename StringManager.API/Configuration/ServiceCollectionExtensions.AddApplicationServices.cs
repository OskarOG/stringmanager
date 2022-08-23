using StringManager.Application.Services.Application;

namespace StringManager.API.Configuration;

internal static partial class ServiceCollectionExtensions
{
    internal static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IUserService, UserService>();
}