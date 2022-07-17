using FluentAssertions;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Value;

public class ObjectNameTests
{
    [Theory, DomainAutoData]
    public void NotEqualOperator_WithUnequalValues_ShouldEvaluateToTrue(
        ObjectName left,
        ObjectName right)
    {
        // Act
        var result = left != right;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void NotEqualOperator_WithEqualValues_ShouldEvaluateToFalse(ObjectName left)
    {
        // Arrange
        var right = ObjectName.Create(left.Value).Value;
        
        // Act
        var result = left != right;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithUnequalValues_ShouldEvaluateToFalse(
        ObjectName left,
        ObjectName right)
    {
        // Act
        var result = left == right;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithEqualValues_ShouldEvaluateToTrue(
        ObjectName left)
    {
        // Arrange
        var right = ObjectName.Create(left.Value).Value;
        
        // Act
        var result = left == right;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void Create_WithValidValues_SetsExpectedProperties(ObjectName val)
    {
        // Act
        var result = ObjectName.Create(val.Value).Value;
        
        // Assert
        result.Value.Should().Be(val.Value);
    }

    [Fact]
    public void Create_WithInvalidValue_ReturnsExpectedProblemType()
    {
        // Act
        var result = ObjectName.Create(null!);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.IsException.Should().BeFalse();
        result.Error.ProblemType.Should().Be(ProblemType.InvalidObjectName);
    }
}