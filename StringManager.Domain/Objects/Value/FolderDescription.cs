using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Domain.Objects.Value;

public class FolderDescription : ValueObject 
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private FolderDescription() {}
#pragma warning restore CS8618
    
    private FolderDescription(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<FolderDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<FolderDescription>.ErrorResult(new Error(ProblemType.EmptyOrNullFolderDescription));
        }

        return Result<FolderDescription>.SuccessResult(new FolderDescription(value));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}