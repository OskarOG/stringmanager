using FluentAssertions;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Value;

public class PasswordTests
{
    [Theory, DomainAutoData]
    public void NotEqualOperator_WithUnequalValues_ShouldEvaluateToTrue(
        string left,
        string right)
    {
        // Arrange
        var leftPassword = Password.NewPassword(left);
        var rightPassword = Password.NewPassword(right);
        
        // Act
        var result = leftPassword != rightPassword;

        // Assert
        result.Should().BeTrue();
    }

    [Theory(Skip = "Invalid for now, fix the operator"), DomainAutoData]
    public void NotEqualOperator_WithEqualValues_ShouldEvaluateToFalse(string val)
    {
        // Arrange
        var leftPassword = Password.NewPassword(val);
        var rightPassword = Password.NewPassword(val);
        
        // Act
        var result = leftPassword != rightPassword;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithUnequalValues_ShouldEvaluateToFalse(
        string left,
        string right)
    {
        // Arrange
        var leftPassword = Password.NewPassword(left);
        var rightPassword = Password.NewPassword(right);
        
        // Act
        var result = leftPassword == rightPassword;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory(Skip = "Invalid for now, fix the operator"), DomainAutoData]
    public void EqualOperator_WithEqualValues_ShouldEvaluateToTrue(string val)
    {
        // Arrange
        var leftPassword = Password.NewPassword(val);
        var rightPassword = Password.NewPassword(val);
        
        // Act
        var result = leftPassword == rightPassword;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void NewPassword_WithString_HashesPasswordAndSetsExpected(string val)
    {
        // Act
        var result = Password.NewPassword(val);

        // Assert
        result.HashedValue.Should().NotBeNull();
    }

    [Theory, DomainAutoData]
    public void VerifyPassword_WithValidPassword_VerifiesToTrue(string pass)
    {
        // Arrange
        var password = Password.NewPassword(pass);

        // Act
        var result = password.VerifyPassword(pass);

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void VerifyPassword_WithInvalidPassword_VerifiesToFalse(string leftPass, string rightPass)
    {
        // Arrange
        var password = Password.NewPassword(leftPass);
        
        // Act
        var result = password.VerifyPassword(rightPass);
        
        // Assert
        result.Should().BeFalse();
    }
}