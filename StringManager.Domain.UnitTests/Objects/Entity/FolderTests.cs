using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Entity;

public class FolderTests
{
    [Theory, DomainAutoData]
    public void Constructor_WithValidValues_ShouldSetPropertiesAsExpected(
        Guid id,
        ObjectName name,
        FolderDescription description)
    {
        // Act
        var result = new Folder(id, name, description);

        // Assert
        result.Id.Should().Be(id);
    }

    [Fact]
    public void TestingRecursion()
    {
        var fixture = new Fixture();
        
        var folder = fixture.Create<Folder>();

        folder.Should().NotBeNull();
    }
}