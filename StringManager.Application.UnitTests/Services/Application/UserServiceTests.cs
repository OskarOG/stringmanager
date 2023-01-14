using System.Threading.Tasks;
using AutoFixture.Xunit2;
using StringManager.Application.Persistence;
using StringManager.Application.Services.Application;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Value;
using StringManager.TestHelpers.Fixtures;
using Xunit;

namespace StringManager.Application.UnitTests.Services.Application;

public class UserServiceTests
{
    [Theory, DomainAutoData]
    public async Task CreateUserAsync_WithNullAccessGroups_ReturnsExpectedUser(
        Email email,
        UserRoleType userRoleType,
        Password password,
        [Frozen] IRepository<AccessGroup> accessGroupRepository,
        UserService sut)
    {
        // Arrange
        
        // Act
        var result = sut.CreateUserAsync(email, userRoleType, password, accessGroupIds: null);

        // Assert
    }
}