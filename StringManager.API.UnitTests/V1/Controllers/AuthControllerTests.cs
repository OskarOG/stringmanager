using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using StringManager.API.V1.Controllers;
using StringManager.Application.Services.Application;
using StringManager.Application.Services.Domain;
using StringManager.Domain.Messages;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.API.UnitTests.V1.Controllers;

public class AuthControllerTests
{
    [Theory, DomainAutoData]
    public async Task CreateUserToken_WithInvalidEmail_ReturnsExpectedBadRequest(
        ProblemDetail problemDetailForInvalidEmail,
        string invalidEmail,
        [PasswordString] string password,
        [Frozen] IProblemDetailFactory problemDetailFactory,
        [Greedy] AuthController sut)
    {
        // Arrange
        problemDetailFactory.CreateProblemDetail(ProblemType.InvalidEmail)
            .Returns(problemDetailForInvalidEmail);

        // Act
        var result = await sut.CreateUserToken(new(invalidEmail, password));

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeOfType<ProblemDetail>()
            .And.BeEquivalentTo(problemDetailForInvalidEmail);
    }

    [Theory, DomainAutoData]
    public async Task CreateUserToken_WithIncorrectPassword_ReturnsExpectedBadRequest(
        ProblemDetail problemDetailForInvalidPassword,
        Email email,
        [PasswordString] string password,
        [Frozen] IProblemDetailFactory problemDetailFactory,
        [Frozen] IAuthenticationService authenticationService,
        [Greedy] AuthController sut)
    {
        // Arrange
        problemDetailFactory.CreateProblemDetail(ProblemType.IncorrectPassword)
            .Returns(problemDetailForInvalidPassword);

        authenticationService.CreateUserTokenAsync(Arg.Is<Email>(x => x.Value == email.Value), password)
            .Returns(Result<string>.ErrorResult(new Error(ProblemType.IncorrectPassword)));
        
        // Act
        var result = await sut.CreateUserToken(new(email.Value, password));
        
        // Assert
        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeOfType<ProblemDetail>()
            .And.BeEquivalentTo(problemDetailForInvalidPassword);
    }

    [Theory, DomainAutoData]
    public async Task CreateUserToken_WithCorrectUserInformation_ReturnsExpectedCreatedToken(
        string expectedToken,
        Email email,
        [PasswordString] string password,
        [Frozen] IAuthenticationService authenticationService,
        [Greedy] AuthController sut)
    {
        // Arrange
        authenticationService.CreateUserTokenAsync(Arg.Is<Email>(x => x.Value == email.Value), password)
            .Returns(Result<string>.SuccessResult(expectedToken));
        
        // Act
        var result = await sut.CreateUserToken(new(email.Value, password));

        // Assert
        result.Should().BeOfType<CreatedResult>()
            .Which.Value.Should().BeEquivalentTo(new { token = expectedToken});
    }

    [Theory, DomainAutoData]
    public async Task CreateUserToken_WithUnexpectedException_ReturnsExpectedInternalError(
        ProblemDetail problemDetailForInternalError,
        Email email,
        [PasswordString] string password,
        [Frozen] IProblemDetailFactory problemDetailFactory,
        [Frozen] IAuthenticationService authenticationService,
        [Greedy] AuthController sut)
    {
        // Arrange
        problemDetailFactory.CreateProblemDetail(ProblemType.Unknown)
            .Returns(problemDetailForInternalError);

        authenticationService.CreateUserTokenAsync(Arg.Is<Email>(x => x.Value == email.Value), password)
            .Returns(Result<string>.ErrorResult(new Error(ProblemType.Unknown, new InvalidProgramException())));
        
        // Act
        var result = await sut.CreateUserToken(new(email.Value, password));
        
        // Assert
        result.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);

        result.As<ObjectResult>()
            .Value.Should().BeEquivalentTo(problemDetailForInternalError);
    }
}