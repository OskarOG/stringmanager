namespace StringManager.API.V1.Messages;

public record NewUserRequest(
    string Email,
    string Password,
    string RoleType,
    Guid[]? AccessGroupIds);