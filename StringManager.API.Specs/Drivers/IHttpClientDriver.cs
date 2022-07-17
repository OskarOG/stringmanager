using System.Net;

namespace StringManager.API.Specs.Drivers;

public interface IHttpClientDriver
{
    string CurrentStringContent { get; }
    
    HttpStatusCode CurrentHttpStatusCode { get; }

    Task SendRequestAsync(HttpMethod method, string endpoint, string? content);
}