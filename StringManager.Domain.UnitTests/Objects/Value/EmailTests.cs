using FluentAssertions;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Attributes;
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
}