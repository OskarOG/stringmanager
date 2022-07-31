using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using StringManager.Application.Persistence;
using StringManager.Application.Services.Application;
using StringManager.Application.Services.Domain;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Application.UnitTests.Services.Application;

public class AuthenticationServiceTests
{
    [Theory, DomainAutoData]
    public async Task CreateUserTokenAsync_WithNonExistingUserForEmail_ReturnsExpectedResult(
        Email email,
        string password,
        AuthenticationService sut)
    {
        // Act
        var result = await sut.CreateUserTokenAsync(email, password);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.ProblemType.Should().Be(ProblemType.NoUserFound);
    }

    [Theory, DomainAutoData]
    public async Task CreateUserTokenAsync_WithMultipleExistingUsersForEmail_ThrowsExpectedException(
        [Frozen] Email email,
        string password,
        ICollection<User> users,
        IRepository<User> userRepository,
        [Frozen] IUnitOfWork unitOfWork,
        AuthenticationService sut)
    {
        // Arrange
        userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>())
            .Returns(users);

        unitOfWork.Repository<User>().Returns(userRepository);

        // Act / Assert
        await sut.Invoking(x => x.CreateUserTokenAsync(email, password))
            .Should()
            .ThrowAsync<InvalidProgramException>()
            .WithMessage("**Multiple users was found for a single email during token creation**");
    }

    [Theory, DomainAutoData]
    public async Task CreateUserTokenAsync_WithInvalidPasswordForUser_ReturnsExpectedFailedResult(
        [Frozen] Email email,
        [PasswordString] string password,
        User user,
        IRepository<User> userRepository,
        [Frozen] IUnitOfWork unitOfWork,
        AuthenticationService sut)
    {
        // Arrange
        userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>())
            .Returns(new[] {user});
        unitOfWork.Repository<User>().Returns(userRepository);
        
        // Act
        var result = await sut.CreateUserTokenAsync(email, password);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.IsException.Should().BeFalse();
        result.Error.ProblemType.Should().Be(ProblemType.IncorrectPassword);
    }

    [Theory, DomainAutoData]
    public async Task CreateUserTokenAsync_WithValidPasswordForUser_ReturnsTheExpectedToken(
        string expectedToken,
        User user,
        [PasswordString] string passwordString,
        IRepository<User> userRepository,
        [Frozen] IUnitOfWork unitOfWork,
        [Frozen] ITokenCreationService tokenCreationService,
        AuthenticationService sut)
    {
        // Arrange
        tokenCreationService.CreateToken(user)
            .Returns(expectedToken);
        
        userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>())
            .Returns(new[] {user});
        unitOfWork.Repository<User>().Returns(userRepository);

        user.Password = Password.Create(passwordString).Value;
        
        // Act
        var result = await sut.CreateUserTokenAsync(user.Email, passwordString);

        // Assert
        tokenCreationService.Received().CreateToken(user);
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedToken);
    }
}