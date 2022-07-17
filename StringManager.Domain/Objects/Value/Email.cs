using System.Text.RegularExpressions;
using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Domain.Objects.Value;

public class Email : ValueObject
{
    private const string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
    
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private Email() {}
#pragma warning restore CS8618
    
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<Email> Create(string value)
    {
        return Regex.IsMatch(value, EmailRegex)
            ? Result<Email>.SuccessResult(new Email(value))
            : Result<Email>.ErrorResult(new Error(ProblemType.InvalidEmail));
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}