namespace StringManager.API.Specs.Support;

public interface IWebApiFactoryWrapper
{
    public HttpClient CreateClient();
}