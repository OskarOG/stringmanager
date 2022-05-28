using System;
using FluentAssertions;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Entity;

public class AccessGroupTests
{
    [Theory, DomainAutoData]
    public void Constructor_WithValidValues_SetsExpectedProperties(
        Guid id,
        ObjectName name)
    {
        // Act
        var result = new AccessGroup(id, name);

        // Assert
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);

        result.Parent.Should().BeNull();
        result.Children.Should().BeEmpty();
        result.Users.Should().BeEmpty();
        result.AccessibleFolders.Should().BeEmpty();
    }
}