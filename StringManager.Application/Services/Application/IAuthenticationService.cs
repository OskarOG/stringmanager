using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;

namespace StringManager.Application.Services.Application;

public interface IAuthenticationService
{
    Task<Result<string>> CreateUserTokenAsync(Email email, string password);
}