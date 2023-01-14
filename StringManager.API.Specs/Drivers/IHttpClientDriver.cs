using System.Net;

namespace StringManager.API.Specs.Drivers;

public interface IHttpClientDriver
{
    string CurrentStringContent { get; }
    
    HttpStatusCode CurrentHttpStatusCode { get; }

    T DeserializeContent<T>() where T : class;

    Task SendRequestAsync(HttpMethod method, string endpoint, object? content);
    
    Task SendRequestAsync(HttpMethod method, string endpoint, string? content);
    
    Task SendRequestWithTokenAsync(HttpMethod method, string endpoint, object? content);
    
    Task SendRequestWithTokenAsync(HttpMethod method, string endpoint, string? content);
}