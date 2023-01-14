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
    
    public async Task<Result<User>> CreateUserAsync(
        Email email,
        UserRoleType userRoleType,
        Password password,
        ICollection<Guid>? accessGroupIds)
    {
        // TODO:
        var accessGroupResult = await FetchAccessGroups(accessGroupIds);
        if (accessGroupResult.IsFailure)
        {
            return Result<User>.ErrorResult(accessGroupResult.Error);
        }
        
        // 3. Get the initial access group ids
        // 4. Check if the signed in user has access to the groups he's trying to give the new user
        // 5. Create new user object with all information

        throw new NotImplementedException();
    }

    private async Task<Result<ICollection<AccessGroup>>> FetchAccessGroups(ICollection<Guid>? accessGroupIds)
    {
        if (accessGroupIds == null || !accessGroupIds.Any())
        {
            return Result<ICollection<AccessGroup>>.SuccessResult(Array.Empty<AccessGroup>());
        }

        var accessGroups = await _unitOfWork.Repository<AccessGroup>()
            .GetAsync(accessGroup => accessGroupIds.Contains(accessGroup.Id));

        if (!accessGroups.All(accessGroup => accessGroupIds.Contains(accessGroup.Id)))
        {
            return Result<ICollection<AccessGroup>>.ErrorResult(new Error(ProblemType.NonExistingAccessGroup));
        }

        
        throw new NotImplementedException();
    }
}