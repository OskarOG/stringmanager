using System.Net;
using Microsoft.Extensions.DependencyInjection;
using StringManager.API.Specs.Support.Exceptions;
using StringManager.Infrastructure.Persistence;
using StringManager.TestHelpers.Fixtures;
using StringManager.TestHelpers.Objects;

namespace StringManager.API.Specs.Drivers;

public class HttpClientDriver : IHttpClientDriver
{
    private readonly StringManagerWebApiFactory _webAppFactory;

    private string? _currentStringContent;
    private HttpStatusCode? _currentHttpStatusCode;
    
    public HttpClientDriver(StringManagerDbContext dbContext)
    {
        _webAppFactory = new StringManagerWebApiFactory(new Dictionary<Type, CustomServiceMock>
        {
            [typeof(StringManagerDbContext)] = new(ServiceLifetime.Scoped, dbContext)
        });
    }

    public string CurrentStringContent
    {
        get => _currentStringContent ?? throw new StepMissingException("No step for setting the http content has been invoked");
        private set => _currentStringContent = value;
    }

    public HttpStatusCode CurrentHttpStatusCode
    {
        get => _currentHttpStatusCode ?? throw new StepMissingException("No step for setting the http status code has been invoked"); 
        private set => _currentHttpStatusCode = value;
    }
    
    public async Task SendRequestAsync(HttpMethod method, string endpoint, string? content)
    {
        var client = _webAppFactory.CreateClient();
        var response = await client.SendAsync(new HttpRequestMessage(method, endpoint)
        {
            Content = content != null ? new StringContent(content) : null,
        });

        CurrentStringContent = await response.Content.ReadAsStringAsync();
        CurrentHttpStatusCode = response.StatusCode;
    }
}