using StringManager.API.Specs.Support.Exceptions;

namespace StringManager.API.Specs.Support.Contexts;

public record AuthContext
{
    private string? _jwt;

    public string Jwt
    {
        get => _jwt ?? throw new StepMissingException("A step for requesting a new token is missing");
        set => _jwt = value;
    }
}