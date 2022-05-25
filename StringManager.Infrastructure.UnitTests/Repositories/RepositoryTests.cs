using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using StringManager.Infrastructure.Persistence;
using StringManager.Infrastructure.UnitTests.Objects;
using StringManager.TestHelpers.Attributes;
using Xunit;

namespace StringManager.Infrastructure.UnitTests.Repositories;

public class RepositoryTests
{
    [Theory, AutoDataWithInMemoryDb]
    public async Task Delete_WithEntity_ShouldRemoveExpectedEntity(
        TestEntity testEntity,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<TestEntity> sut)
    {
        // Arrange
        stringManagerDbContext.Set<TestEntity>().Add(testEntity);
        await stringManagerDbContext.SaveChangesAsync();

        // Act
        sut.Delete(testEntity);
        await stringManagerDbContext.SaveChangesAsync();

        // Assert
        stringManagerDbContext
            .Set<TestEntity>()
            .Should()
            .NotContain(x => x.Id == testEntity.Id);
    }
}