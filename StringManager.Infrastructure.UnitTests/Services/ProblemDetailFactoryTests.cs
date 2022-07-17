using System;
using System.Net;
using FluentAssertions;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Infrastructure.Services;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Infrastructure.UnitTests.Services;

public class ProblemDetailFactoryTests
{
    [Theory, DomainAutoData]
    public void CreateProblemDetail_WithOnlyProblemType_ReturnsExpectedProblemDetail(
        ProblemDetailFactory problemDetailFactory)
    {
        // Act
        var result = problemDetailFactory.CreateProblemDetail(ProblemType.InvalidEmail);

        // Assert
        result.Detail.Should().NotBeNullOrWhiteSpace();
        result.Title.Should().NotBeNullOrWhiteSpace();
        result.ProblemType.Should().Be(Enum.GetName(ProblemType.InvalidEmail));
        result.StatusCode.Should().BeNull();
    }

    [Theory, DomainAutoData]
    public void CreateProblemDetail_WithProblemTypeAndStatusCode_ReturnsExpectedProblemDetail(
        ProblemDetailFactory problemDetailFactory)
    {
        // Act
        var result = problemDetailFactory.CreateProblemDetail(ProblemType.InvalidObjectName, HttpStatusCode.OK);

        // Assert
        result.Detail.Should().NotBeNullOrWhiteSpace();
        result.Title.Should().NotBeNullOrWhiteSpace();
        result.ProblemType.Should().Be(Enum.GetName(ProblemType.InvalidObjectName));
        result.StatusCode.Should().Be((int) HttpStatusCode.OK);
    }

    [Theory, DomainAutoData]
    public void CreateProblemDetail_WithInvalidProblemType_ReturnsExpectedException(
        ProblemDetailFactory problemDetailFactory)
    {
        // Act / Assert
        problemDetailFactory.Invoking(x => x.CreateProblemDetail(ProblemType.Unknown))
            .Should().Throw<NullReferenceException>();
    }
    
    [Theory, DomainAutoData]
    public void CreateProblemDetail_WithInvalidProblemTypeAndStatusCode_ReturnsExpectedException(
        ProblemDetailFactory problemDetailFactory)
    {
        // Act / Assert
        problemDetailFactory.Invoking(x => x.CreateProblemDetail(ProblemType.Unknown, HttpStatusCode.OK))
            .Should().Throw<NullReferenceException>();
    }
}