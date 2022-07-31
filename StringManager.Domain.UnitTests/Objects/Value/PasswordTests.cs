using AutoFixture.Xunit2;
using FluentAssertions;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Value;

public class PasswordTests
{
    [Theory, DomainAutoData]
    public void NotEqualOperator_WithUnequalValues_ShouldEvaluateToTrue(
        Password left,
        Password right)
    {
        // Act
        var result = left != right;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void NotEqualOperator_WithEqualValues_ShouldEvaluateToFalse(
        [Frozen] Password leftPassword,
        Password rightPassword)
    {
        // Act
        var result = leftPassword != rightPassword;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithUnequalValues_ShouldEvaluateToFalse(
        Password leftPassword,
        Password rightPassword)
    {
        // Act
        var result = leftPassword == rightPassword;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithEqualValues_ShouldEvaluateToTrue(
        [Frozen] Password leftPassword,
        Password rightPassword)
    {
        // Act
        var result = leftPassword == rightPassword;

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("HelloWorld123!")]
    public void NewPassword_WithString_HashesPasswordAndSetsExpected(string val)
    {
        // Act
        var result = Password.Create(val).Value;

        // Assert
        result.HashedValue.Should().NotBeNull();
        result.VerifyPassword(val).Should().BeTrue();
    }

    [Theory]
    [InlineData("HelloWorld123!")]
    public void VerifyPassword_WithValidPassword_VerifiesToTrue(string pass)
    {
        // Arrange
        var password = Password.Create(pass).Value;

        // Act
        var result = password.VerifyPassword(pass);

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void VerifyPassword_WithInvalidPassword_VerifiesToFalse(string leftPass, string rightPass)
    {
        // Arrange
        var password = Password.Create(leftPass).Value;
        
        // Act
        var result = password.VerifyPassword(rightPass);
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("hello")]
    [InlineData("123")]
    [InlineData("hello12")]
    [InlineData("helloWorld")]
    [InlineData("hello12345678999000")]
    [InlineData("hello12345678999000!!!")]
    public void NewPassword_WithInvalidString_ReturnsFailedResult(string invalidPassword)
    {
        // Act
        var result = Password.Create(invalidPassword);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.ProblemType.Should().Be(ProblemType.InvalidNewPassword);
    }
}