using StringManager.Domain.Objects.Entity;

namespace StringManager.Application.Services.Domain;

public interface ITokenCreationService
{
    string CreateToken(User user);
}