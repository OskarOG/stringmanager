using Microsoft.Extensions.DependencyInjection;
using StringManager.Application.Persistence;
using StringManager.Application.Services.Infrastructure;
using StringManager.Infrastructure.Persistence;
using StringManager.TestHelpers.Fixtures;
using StringManager.TestHelpers.Objects;

namespace StringManager.API.Specs.Support;

public class WebApiFactoryWrapper : IWebApiFactoryWrapper
{
    private readonly StringManagerWebApiFactory _factory;
    
    public WebApiFactoryWrapper(
        StringManagerDbContext dbContext,
        IDateTimeService dateTimeService)
    {
        _factory = new StringManagerWebApiFactory(new Dictionary<Type, CustomServiceMock>
        {
            [typeof(IUnitOfWork)] = new(ServiceLifetime.Scoped, dbContext),
            [typeof(IDateTimeService)] = new(ServiceLifetime.Transient, dateTimeService)
        });
    }

    public HttpClient CreateClient() => _factory.CreateClient();
}