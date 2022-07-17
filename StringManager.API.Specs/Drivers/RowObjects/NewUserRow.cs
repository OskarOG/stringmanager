using StringManager.Domain.Objects.Value;

namespace StringManager.API.Specs.Drivers.RowObjects;

public class NewUserRow
{
    public NewUserRow(
        string email,
        string password,
        UserRoleType roleType)
    {
        Email = email;
        Password = password;
        RoleType = roleType;
    }

    public string Email { get; }

    public string Password { get; }

    public UserRoleType RoleType { get; }
}