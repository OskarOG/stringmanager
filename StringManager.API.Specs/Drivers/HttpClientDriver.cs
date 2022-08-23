using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StringManager.API.Specs.Support.Exceptions;
using StringManager.Application.Persistence;
using StringManager.Application.Services.Infrastructure;
using StringManager.Infrastructure.Persistence;
using StringManager.TestHelpers.Fixtures;
using StringManager.TestHelpers.Objects;

namespace StringManager.API.Specs.Drivers;

public class HttpClientDriver : IHttpClientDriver
{
    private readonly StringManagerWebApiFactory _webAppFactory;

    private string? _currentStringContent;
    private HttpStatusCode? _currentHttpStatusCode;
    private object? _currentDeserializedContent;

    public HttpClientDriver(
        StringManagerDbContext dbContext,
        IDateTimeService dateTimeService)
    {
        _webAppFactory = new StringManagerWebApiFactory(new Dictionary<Type, CustomServiceMock>
        {
            [typeof(IUnitOfWork)] = new(ServiceLifetime.Scoped, dbContext),
            [typeof(IDateTimeService)] = new(ServiceLifetime.Transient, dateTimeService)
        });
    }

    public string CurrentStringContent
    {
        get => _currentStringContent ??
               throw new StepMissingException("No step for setting the http content has been invoked");
        private set => _currentStringContent = value;
    }

    public HttpStatusCode CurrentHttpStatusCode
    {
        get => _currentHttpStatusCode ??
               throw new StepMissingException("No step for setting the http status code has been invoked");
        private set => _currentHttpStatusCode = value;
    }

    public T DeserializeContent<T>()
        where T : class
    {
        _currentDeserializedContent ??= JsonConvert.DeserializeObject<T>(CurrentStringContent)
            ?? throw new ArgumentException("Unable to deserialize current HTTP content to expected type");

        return (T) _currentDeserializedContent;
    }

    public async Task SendRequestAsync(HttpMethod method, string endpoint, object? content) =>
        await SendRequestAsync(method, endpoint, JsonConvert.SerializeObject(content));

    public async Task SendRequestAsync(HttpMethod method, string endpoint, string? content)
    {
        var client = _webAppFactory.CreateClient();
        var response = await client.SendAsync(new HttpRequestMessage(method, endpoint)
        {
            Content = content != null ? new StringContent(content, Encoding.UTF8, "application/json") : null
        });

        CurrentStringContent = await response.Content.ReadAsStringAsync();
        CurrentHttpStatusCode = response.StatusCode;
    }
}