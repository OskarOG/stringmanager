namespace StringManager.API.V1.Messages;

public record UserTokenRequest(
    string Email,
    string Password);