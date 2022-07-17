namespace StringManager.API.V1.Messages;

public class NewUserResponse
{
    public NewUserResponse(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}