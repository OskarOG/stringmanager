using StringManager.Application.Persistence;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Infrastructure;
using StringManager.Domain.Objects.Value;

namespace StringManager.Application.Services.Application;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<Result<User>> CreateUserAsync(
        Email email, 
        UserRoleType userRoleType,
        Password password,
        ICollection<Guid>? accessGroupIds)
    {
        // TODO:
        // 1. Get the signed in user id
        // 2. Check if the signed in user has access to create new users
        // 3. Get the initial access group ids
        // 4. Check if the signed in user has access to the groups he's trying to give the new user
        // 5. Create new user object with all information

        throw new NotImplementedException();
    }
}