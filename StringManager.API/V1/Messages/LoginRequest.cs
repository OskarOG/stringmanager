namespace StringManager.API.V1.Messages;

public record LoginRequest(
    string Email,
    string Password);