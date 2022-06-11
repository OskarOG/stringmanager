using AutoFixture;
using StringManager.API.Specs.Drivers.RowObjects;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Value;
using StringManager.Infrastructure.Persistence;

namespace StringManager.API.Specs.Drivers;

public class DatabaseDriver
{
    private readonly StringManagerDbContext _dbContext;
    private readonly IFixture _fixture;

    public DatabaseDriver(
        StringManagerDbContext dbContext,
        Fixture fixture)
    {
        _dbContext = dbContext;
        _fixture = fixture;
    }

    public async Task AddExistingUsersToDatabaseAsync(IEnumerable<ExistingUserRow> userRows)
    {
        var userSet = _dbContext.Set<User>();
        
        foreach (var existingUserRow in userRows)
        {
            userSet.Add(
                new User(
                    existingUserRow.UserId,
                    new Email(existingUserRow.Email),
                    UserRoleType.User,
                    Password.NewPassword(_fixture.Create<string>())));
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRolesToExistingUsersAsync(IEnumerable<UserRoleRow> userRoleRows)
    {
        var userSet = _dbContext.Set<User>();
        
        foreach (var userRoleRow in userRoleRows)
        {
            var currentUser = await userSet.FindAsync(userRoleRow.UserId)
                ?? throw new NullReferenceException();
            
            currentUser.UserRole = userRoleRow.RoleType;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task AddExistingAccessGroupsAsync(IEnumerable<AccessGroupRow> accessGroupRows)
    {
        var accessGroupSet = _dbContext.Set<AccessGroup>();
        
        foreach (var accessGroupRow in accessGroupRows)
        {
            accessGroupSet.Add(
                new AccessGroup(
                    accessGroupRow.AccessGroupId,
                    new ObjectName(accessGroupRow.AccessGroupName)));
        }

        await _dbContext.SaveChangesAsync();
    }
}