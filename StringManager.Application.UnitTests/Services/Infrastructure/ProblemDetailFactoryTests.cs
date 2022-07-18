using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using StringManager.Application.Services.Domain;
using StringManager.Application.Services.Infrastructure;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Application.UnitTests.Services.Infrastructure;

public class ProblemDetailFactoryTests
{
    [Theory, DomainAutoData]
    public void CreateProblemDetail_WithOnlyProblemType_ReturnsExpectedProblemDetail(
        string detail,
        string title,
        [Frozen] IProblemDetailTextService problemDetailTextService,
        ProblemDetailFactory problemDetailFactory)
    {
        // Arrange
        problemDetailTextService.GetText(Arg.Is<string>("detail-InvalidEmail"))
            .Returns(detail);
        problemDetailTextService.GetText(Arg.Is<string>("title-InvalidEmail"))
            .Returns(title);
        
        // Act
        var result = problemDetailFactory.CreateProblemDetail(ProblemType.InvalidEmail);

        // Assert
        result.Detail.Should().Be(detail);
        result.Title.Should().Be(title);
        result.ProblemType.Should().Be(Enum.GetName(ProblemType.InvalidEmail));
    }

    [Theory, DomainAutoData]
    public void CreateProblemDetail_WithInvalidProblemType_ReturnsExpectedExceptionForNoTitle(
        [Frozen] IProblemDetailTextService problemDetailTextService,
        ProblemDetailFactory problemDetailFactory)
    {
        // Arrange
        problemDetailTextService.GetText(Arg.Is("title-Unknown"))
            .Returns(info => null);
            
        // Act / Assert
        problemDetailFactory.Invoking(x => x.CreateProblemDetail(ProblemType.Unknown))
            .Should().Throw<NullReferenceException>()
            .WithMessage("**title**");
    }
    
    [Theory, DomainAutoData]
    public void CreateProblemDetail_WithInvalidProblemType_ReturnsExpectedExceptionForNoDetail(
        [Frozen] IProblemDetailTextService problemDetailTextService,
        ProblemDetailFactory problemDetailFactory)
    {
        // Arrange
        problemDetailTextService.GetText(Arg.Is("detail-Unknown"))
            .Returns(info => null);
            
        // Act / Assert
        problemDetailFactory.Invoking(x => x.CreateProblemDetail(ProblemType.Unknown))
            .Should().Throw<NullReferenceException>()
            .WithMessage("**detail**");
    }
}