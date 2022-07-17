using System.Net;
using FluentAssertions;
using StringManager.API.Specs.Drivers;

namespace StringManager.API.Specs.Steps;

[Binding]
public class HttpResponseSteps
{
    private readonly IHttpClientDriver _httpClientDriver;
    
    public HttpResponseSteps(IHttpClientDriver httpClientDriver)
    {
        _httpClientDriver = httpClientDriver;
    }
    
    [Then(@"the http status code ""(.*)"" is returned")]
    public void ThenTheHttpStatusCodeIsReturned(string httpStatusString)
    {
        _httpClientDriver.CurrentHttpStatusCode
            .Should().Be(ParseHttpStatusString(httpStatusString));
    }

    private static HttpStatusCode ParseHttpStatusString(string httpStatusString) => 
        (HttpStatusCode)int.Parse(
            httpStatusString
                .TakeWhile(x => x is >= '0' and <= '9')
                .ToString()!);
}