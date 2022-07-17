using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Domain.Objects.Value;

public class UserRoleWrapper : ValueObject
{
    public UserRoleWrapper(UserRoleType type) => Type = type;

    public UserRoleType Type { get; }

    public static Result<UserRoleWrapper> Parse(string userRoleType) =>
        Enum.TryParse<UserRoleType>(userRoleType, out var type)
            ? Result<UserRoleWrapper>.SuccessResult(new UserRoleWrapper(type))
            : Result<UserRoleWrapper>.ErrorResult(new Error(ProblemType.UnableToParseUserRoleType)); 
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
    }
}