using FluentAssertions;
using Newtonsoft.Json;
using StringManager.API.Specs.Drivers;
using StringManager.API.V1.Messages;
using StringManager.Domain.Messages;
using TechTalk.SpecFlow.Assist;

namespace StringManager.API.Specs.Steps;

[Binding]
public class ProblemDetailSteps
{
    private readonly IHttpClientDriver _httpClientDriver;
    
    public ProblemDetailSteps(IHttpClientDriver httpClientDriver)
    {
        _httpClientDriver = httpClientDriver;
    }
    
    [Then(@"the following problem detail is returned")]
    public void ThenTheFollowingProblemDetailIsReturned(Table table)
    {
        var expectedProblemDetail = table.CreateInstance<ProblemDetail>();
        var problemDetail = JsonConvert.DeserializeObject<ProblemDetail>(_httpClientDriver.CurrentStringContent)
                            ?? throw new NullReferenceException("Unable to parse json to expected ProblemDetail");

        problemDetail.Detail.Should().Be(expectedProblemDetail.Detail);
        problemDetail.Title.Should().Be(expectedProblemDetail.Title);
        problemDetail.ProblemType.Should().Be(expectedProblemDetail.ProblemType);
    }
}