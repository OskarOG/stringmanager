using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Value;
using StringManager.Infrastructure.Persistence;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Infrastructure.UnitTests.Persistence;

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

    [Theory, AutoDataWithInMemoryDb]
    public async Task Delete_WithDetachedEntity_ShouldRemoveExpectedEntity(
        Folder testEntity,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Arrange
        stringManagerDbContext.Set<Folder>().Add(testEntity);
        await stringManagerDbContext.SaveChangesAsync();
        stringManagerDbContext.Entry(testEntity).State = EntityState.Detached;
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

    [Theory, AutoDataWithInMemoryDb]
    public async Task GetAsyncStringInclude_WithNoInput_ReturnsAllItems(
        ICollection<Folder> testEntities,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Arrange
        await stringManagerDbContext.AddRangeAsync(testEntities);
        await stringManagerDbContext.SaveChangesAsync();
        
        // Act
        var result = await sut.GetAsync();

        // Assert
        result.Should().BeEquivalentTo(testEntities);
    }

    [Theory, AutoDataWithInMemoryDb]
    public async Task GetAsyncStringInclude_WithOrderFilter_ReturnsAllItemsInSpecificOrder(
        ICollection<Folder> testEntities,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Arrange
        var expectedResult = testEntities
            .ToList()
            .OrderBy(x => x.Id);
        
        await stringManagerDbContext.AddRangeAsync(testEntities);
        await stringManagerDbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAsync(
            folders => folders.OrderBy(x => x.Id));

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Theory, AutoDataWithInMemoryDb]
    public async Task GetAsyncStringInclude_WithSelectionFilter_ReturnsAllMatchingItems(
        Folder expectedFolder,
        ICollection<Folder> testEntities,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Arrange
        await stringManagerDbContext.AddRangeAsync(testEntities);
        await stringManagerDbContext.AddAsync(expectedFolder);
        await stringManagerDbContext.SaveChangesAsync();

        // Act
        var result = await sut.GetAsync(filter: folder => folder.Id == expectedFolder.Id);
        
        // Assert
        result
            .Should()
            .HaveCount(1)
            .And
            .OnlyContain(resultFolder => resultFolder.Id == expectedFolder.Id);
    }

    [Theory, AutoDataWithInMemoryDb]
    public async Task GetAsyncStringInclude_WithSelectionFilterAndOrderByFilter_ReturnsAllMatchingItemsInExpectedOrder(
        ICollection<Folder> testEntities,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Arrange
        await stringManagerDbContext.AddRangeAsync(testEntities);
        await stringManagerDbContext.SaveChangesAsync();
        
        // Act
        var result = await sut.GetAsync(
            filter: folder => testEntities.Contains(folder),
            orderBy: folders => folders.OrderBy(x => x.Id));
        
        // Assert
        var expectedList = testEntities.ToList().OrderBy(x => x.Id);
        result.Should().BeEquivalentTo(expectedList);
    }

    [Theory, AutoDataWithInMemoryDb]
    public async Task Insert_WithValidValue_InsertsExpectedEntity(
        Folder testEntity,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Act
        var result = sut.Insert(testEntity);
        await stringManagerDbContext.SaveChangesAsync();
        
        // Assert
        result.Should().BeEquivalentTo(testEntity);
        stringManagerDbContext.Set<Folder>().Should().Contain(testEntity);
    }

    [Theory, AutoDataWithInMemoryDb]
    public async Task Update_WithNewValues_UpdatesExpectedEntity(
        Folder testEntity,
        ObjectName newName,
        FolderDescription newDescription,
        [Frozen] StringManagerDbContext stringManagerDbContext,
        Repository<Folder> sut)
    {
        // Arrange
        await stringManagerDbContext.AddAsync(testEntity);
        await stringManagerDbContext.SaveChangesAsync();

        testEntity.Name = newName;
        testEntity.Description = newDescription;
        
        // Act
        sut.Update(testEntity);
        await stringManagerDbContext.SaveChangesAsync();

        // Assert
        var resultEntity = await stringManagerDbContext.Set<Folder>()
            .FindAsync(testEntity.Id);

        resultEntity.Should().NotBeNull();
        resultEntity!.Name.Should().Be(newName);
        resultEntity!.Description.Should().Be(newDescription);
    }
}