using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StringManager.TestHelpers.Exceptions;
using StringManager.TestHelpers.Objects;

namespace StringManager.TestHelpers.Fixtures;

public class StringManagerWebApiFactory : WebApplicationFactory<Program>
{
    private readonly IDictionary<Type, CustomServiceMock>? _customServiceMocks;
    
    public StringManagerWebApiFactory(IDictionary<Type, CustomServiceMock> customServiceMocks)
    {
        _customServiceMocks = customServiceMocks;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        
        builder.ConfigureServices(services =>
        {
            ReplaceCustomServiceMocks(services, _customServiceMocks);
        });
        
        return base.CreateHost(builder);
    }

    private static void ReplaceCustomServiceMocks(IServiceCollection services, IDictionary<Type, CustomServiceMock>? serviceMocks)
    {
        if (serviceMocks == null)
        {
            return;
        }

        foreach (var mock in serviceMocks)
        {
            var descriptor = services.SingleOrDefault(x => x.ServiceType == mock.Key)
                             ?? throw new InvalidTestSetupException(
                                 $"Unable to find service {mock.Key.FullName} in ServiceCollection");
            services.Remove(descriptor);
            
            AddServiceWithLifetime(services, mock.Key, mock.Value.ServiceMock, mock.Value.Lifetime);
        }
    }

    private static void AddServiceWithLifetime(
        IServiceCollection services,
        Type serviceType,
        object mock,
        ServiceLifetime lifetime)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Scoped:
                services.AddScoped(serviceType, _ => mock);
            break;
            case ServiceLifetime.Singleton:
                services.AddSingleton(serviceType, _ => mock);
            break;
            case ServiceLifetime.Transient:
                services.AddTransient(serviceType,_ => mock);
            break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
    }
}