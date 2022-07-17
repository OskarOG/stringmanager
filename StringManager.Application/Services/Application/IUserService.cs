using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;

namespace StringManager.Application.Services.Application;

public interface IUserService
{
    Task<Result<User>> CreateUserAsync(
        Email email,
        UserRoleType userRoleType,
        Password password,
        ICollection<Guid>? accessGroupIds);
}