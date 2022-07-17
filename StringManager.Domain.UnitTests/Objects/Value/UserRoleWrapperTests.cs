using System;
using FluentAssertions;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Value;

public class UserRoleWrapperTests
{
    [Theory, DomainAutoData]
    public void NotEqualOperator_WithUnequalValues_ShouldEvaluateToTrue(
        UserRoleWrapper left,
        UserRoleWrapper right)
    {
        // Act
        var result = left != right;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void NotEqualOperator_WithEqualValues_ShouldEvaluateToFalse(UserRoleWrapper left)
    {
        // Arrange
        var right = new UserRoleWrapper(left.Type);
        
        // Act
        var result = left != right;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithUnequalValues_ShouldEvaluateToFalse(
        UserRoleWrapper left,
        UserRoleWrapper right)
    {
        // Act
        var result = left == right;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithEqualValues_ShouldEvaluateToTrue(
        UserRoleWrapper left)
    {
        // Arrange
        var right = new UserRoleWrapper(left.Type);
        
        // Act
        var result = left == right;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void Parse_WithInvalidString_ReturnsExpectedFail(
        string invalidType)
    {
        // Act
        var result = UserRoleWrapper.Parse(invalidType);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.ProblemType.Should().Be(ProblemType.UnableToParseUserRoleType);
        result.Error.IsException.Should().BeFalse();

        var act = () =>
        {
             var exception = result.Error.Exception;
        };

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("No exception is set for this error");
    }

    [Theory, DomainAutoData]
    public void Parse_WithValidString_ReturnsExpectedType(UserRoleType type)
    {
        // Arrange
        var typeString = Enum.GetName(type) ?? throw new NullReferenceException("Unable to get name for type");
        
        // Act
        var result = UserRoleWrapper.Parse(typeString);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.Value.Type.Should().Be(type);

        var act = () =>
        {
            var error = result.Error;
        };

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("No error is set for this result");
    }
}