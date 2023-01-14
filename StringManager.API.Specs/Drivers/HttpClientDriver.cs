using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using StringManager.API.Specs.Support;
using StringManager.API.Specs.Support.Contexts;
using StringManager.API.Specs.Support.Exceptions;

namespace StringManager.API.Specs.Drivers;

public class HttpClientDriver : IHttpClientDriver
{
    private readonly AuthContext _authContext;
    private readonly IWebApiFactoryWrapper _webApiFactoryWrapper;
    
    private string? _currentStringContent;
    private HttpStatusCode? _currentHttpStatusCode;
    private object? _currentDeserializedContent;

    public HttpClientDriver(
        AuthContext authContext,
        IWebApiFactoryWrapper webApiFactoryWrapper)
    {
        _authContext = authContext;
        _webApiFactoryWrapper = webApiFactoryWrapper;
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
        var client = _webApiFactoryWrapper.CreateClient();
        var response = await client.SendAsync(new HttpRequestMessage(method, endpoint)
        {
            Content = content != null ? new StringContent(content, Encoding.UTF8, "application/json") : null
        });

        CurrentStringContent = await response.Content.ReadAsStringAsync();
        CurrentHttpStatusCode = response.StatusCode;
    }

    public async Task SendRequestWithTokenAsync(HttpMethod method, string endpoint, object? content) =>
        await SendRequestWithTokenAsync(method, endpoint, JsonConvert.SerializeObject(content));

    public async Task SendRequestWithTokenAsync(HttpMethod method, string endpoint, string? content)
    {
        var client = _webApiFactoryWrapper.CreateClient();
        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_authContext.Jwt}");
        var response = await client.SendAsync(
            new HttpRequestMessage(method, endpoint)
            {
                Content = content != null ? new StringContent(content, Encoding.UTF8, "application/json") : null
            });

        CurrentStringContent = await response.Content.ReadAsStringAsync();
        CurrentHttpStatusCode = response.StatusCode;
    }
}