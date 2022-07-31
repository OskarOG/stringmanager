using System.Text.RegularExpressions;
using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Domain.Objects.Value;

public class Password : ValueObject
{
    private const string PasswordRegex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
    
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private Password() {}
#pragma warning restore CS8618
    
    private Password(string hashedValue)
    {
        HashedValue = hashedValue;
    }

    public string HashedValue { get; private set; }

    public static Result<Password> Create(string password) =>
        Regex.IsMatch(password, PasswordRegex)
            ? Result<Password>.SuccessResult(
                new Password(
                    BCrypt.Net.BCrypt.EnhancedHashPassword(password)))
            : Result<Password>.ErrorResult(new Error(ProblemType.InvalidNewPassword));

    public bool VerifyPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, HashedValue);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return HashedValue;
    }
}