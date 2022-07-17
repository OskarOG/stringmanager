using FluentAssertions;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Value;

public class EmailTests
{
    [Theory, DomainAutoData]
    public void NotEqualOperator_WithUnequalValues_ShouldEvaluateToTrue(
        Email leftEmail,
        Email rightEmail)
    {
        // Act
        var result = leftEmail != rightEmail;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void NotEqualOperator_WithEqualValues_ShouldEvaluateToFalse(Email leftEmail)
    {
        // Arrange
        var rightEmail = Email.Create(leftEmail.Value).Value;
        
        // Act
        var result = leftEmail != rightEmail;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithUnequalValues_ShouldEvaluateToFalse(
        Email leftEmail,
        Email rightEmail)
    {
        // Act
        var result = leftEmail == rightEmail;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithEqualValues_ShouldEvaluateToTrue(
        Email leftEmail)
    {
        // Arrange
        var rightEmail = Email.Create(leftEmail.Value).Value;
        
        // Act
        var result = leftEmail == rightEmail;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void Create_WithValidValues_SetsExpectedProperties(Email email)
    {
        // Act
        var result = Email.Create(email.Value);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Value.Should().Be(email.Value);
    }

    [Theory, DomainAutoData]
    public void Create_WithInvalidValue_SetsExpectedFailureResult(string invalidEmail)
    {
        // Act
        var result = Email.Create(invalidEmail);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.IsException.Should().BeFalse();
        result.Error.ProblemType.Should().Be(ProblemType.InvalidEmail);
    }
}