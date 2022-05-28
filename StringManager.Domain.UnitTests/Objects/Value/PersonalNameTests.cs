using FluentAssertions;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Value;

public class PersonalNameTests
{
    [Theory, DomainAutoData]
    public void NotEqualOperator_WithUnequalValues_ShouldEvaluateToTrue(
        PersonalName left,
        PersonalName right)
    {
        // Act
        var result = left != right;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void NotEqualOperator_WithEqualValues_ShouldEvaluateToFalse(PersonalName left)
    {
        // Arrange
        var right = new PersonalName(left.Forename, left.Surname);
        
        // Act
        var result = left != right;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithUnequalValues_ShouldEvaluateToFalse(
        PersonalName left,
        PersonalName right)
    {
        // Act
        var result = left == right;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithEqualValues_ShouldEvaluateToTrue(
        PersonalName left)
    {
        // Arrange
        var right = new PersonalName(left.Forename, left.Surname);
        
        // Act
        var result = left == right;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void Constructor_WithValidValues_SetsExpectedProperties(PersonalName val)
    {
        // Act
        var result = new PersonalName(val.Forename, val.Surname);
        
        // Assert
        result.Forename.Should().Be(val.Forename);
        result.Surname.Should().Be(val.Surname);
        result.Forename.Should().Be(val.Forename);
    }
}