using StringManager.Domain.Objects.Value;

namespace StringManager.API.Specs.Drivers.RowObjects;

public class UserRoleRow
{
    public UserRoleRow(Guid userId, UserRoleType roleType)
    {
        UserId = userId;
        RoleType = roleType;
    }

    public Guid UserId { get; }

    public UserRoleType RoleType { get; }
}