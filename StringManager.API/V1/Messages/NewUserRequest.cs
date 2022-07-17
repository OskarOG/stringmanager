namespace StringManager.API.V1.Messages;

public class NewUserRequest
{
    public NewUserRequest(
        string email,
        string password,
        string roleType,
        Guid[]? accessGroupIds)
    {
        Email = email;
        Password = password;
        RoleType = roleType;
        AccessGroupIds = accessGroupIds;
    }

    public string Email { get; }

    public string Password { get; }

    public string RoleType { get; }

    public Guid[]? AccessGroupIds { get; }
}