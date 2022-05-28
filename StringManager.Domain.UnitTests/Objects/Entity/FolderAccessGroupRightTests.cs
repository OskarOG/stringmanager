using FluentAssertions;
using StringManager.Domain.Objects.Entity;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Domain.UnitTests.Objects.Entity;

public class FolderAccessGroupRightTests
{
    [Theory, DomainAutoData]
    public void Constructor_WithValidValues_SetsExpectedProperties(
        Folder folder,
        AccessGroup accessGroup)
    {
        // Act
        var result = new FolderAccessGroupRight(folder, accessGroup);

        // Assert
        result.Folder.Should().Be(folder);
        result.AccessGroup.Should().Be(accessGroup);
        result.AccessRights.Should().BeEmpty();
    }
}