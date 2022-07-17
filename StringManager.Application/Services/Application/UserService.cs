using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;

namespace StringManager.Application.Services.Application;

public class UserService : IUserService
{
    public Task<Result<User>> CreateUserAsync(
        Email email, 
        UserRoleType userRoleType,
        Password password,
        ICollection<Guid>? accessGroupIds)
    {
        throw new NotImplementedException();
    }
}