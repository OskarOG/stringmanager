namespace StringManager.API.Specs.Drivers.RowObjects;

public class ExistingUserRow
{
    public ExistingUserRow(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }

    public Guid UserId { get; }

    public string Email { get; }
}