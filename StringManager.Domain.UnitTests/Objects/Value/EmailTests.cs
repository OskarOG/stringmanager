using FluentAssertions;
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
        var rightEmail = new Email(leftEmail.Value);
        
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
        var rightEmail = new Email(leftEmail.Value);
        
        // Act
        var result = leftEmail == rightEmail;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void Constructor_WithValidValues_SetsExpectedProperties(Email email)
    {
        // Act
        var result = new Email(email.Value);
        
        // Assert
        result.Value.Should().Be(email.Value);
    }
}