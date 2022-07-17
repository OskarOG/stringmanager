using FluentAssertions;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Value;

public class FolderDescriptionTests
{
    [Theory, DomainAutoData]
    public void NotEqualOperator_WithUnequalValues_ShouldEvaluateToTrue(
        FolderDescription leftDescription,
        FolderDescription rightDescription)
    {
        // Act
        var result = leftDescription != rightDescription;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void NotEqualOperator_WithEqualValues_ShouldEvaluateToFalse(FolderDescription leftDescription)
    {
        // Arrange
        var rightDescription = FolderDescription.Create(leftDescription.Value).Value;
        
        // Act
        var result = leftDescription != rightDescription;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithUnequalValues_ShouldEvaluateToFalse(
        FolderDescription leftDescription,
        FolderDescription rightDescription)
    {
        // Act
        var result = leftDescription == rightDescription;
        
        // Assert
        result.Should().BeFalse();
    }

    [Theory, DomainAutoData]
    public void EqualOperator_WithEqualValues_ShouldEvaluateToTrue(
        FolderDescription leftDescription)
    {
        // Arrange
        var rightDescription = FolderDescription.Create(leftDescription.Value).Value;
        
        // Act
        var result = leftDescription == rightDescription;

        // Assert
        result.Should().BeTrue();
    }

    [Theory, DomainAutoData]
    public void Create_WithValidValues_SetsExpectedProperties(FolderDescription description)
    {
        // Act
        var result = FolderDescription.Create(description.Value).Value;
        
        // Assert
        result.Value.Should().Be(description.Value);
    }

    [Fact]
    public void Create_WithInvalidValue_ReturnsExpectedProblemType()
    {
        // Act
        var result = FolderDescription.Create(null!);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.IsException.Should().BeFalse();
        result.Error.ProblemType.Should().Be(ProblemType.EmptyOrNullFolderDescription);
    }
}