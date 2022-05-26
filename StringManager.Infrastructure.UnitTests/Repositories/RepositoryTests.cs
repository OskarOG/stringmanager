using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using StringManager.Domain.Objects.Entity;
using StringManager.Infrastructure.Persistence;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Infrastructure.UnitTests.Repositories;

public class RepositoryTests
{
    [Theory, AutoDataWithInMemoryDb]
    public async Task Delete_WithEntity_ShouldRemoveExpectedEntity(
        Folder testEntity,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Arrange
        stringManagerDbContext.Set<Folder>().Add(testEntity);
        await stringManagerDbContext.SaveChangesAsync();

        // Act
        sut.Delete(testEntity);
        await stringManagerDbContext.SaveChangesAsync();

        // Assert
        stringManagerDbContext
            .Set<Folder>()
            .Should()
            .NotContain(x => x.Id == testEntity.Id);
    }
}