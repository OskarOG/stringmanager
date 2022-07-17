using StringManager.API.Specs.Drivers.RowObjects;

namespace StringManager.API.Specs.Drivers;

public interface IDatabaseDriver
{
    Task AddExistingUsersToDatabaseAsync(IEnumerable<ExistingUserRow> userRows);

    Task AddRolesToExistingUsersAsync(IEnumerable<UserRoleRow> userRoleRows);

    Task AddExistingAccessGroupsAsync(IEnumerable<AccessGroupRow> accessGroupRows);
}