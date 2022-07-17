using StringManager.Domain.Objects.Infrastructure;

namespace StringManager.Domain.Objects.Value;

public class ObjectName : ValueObject
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private ObjectName() {}
#pragma warning restore CS8618
    
    private ObjectName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<ObjectName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<ObjectName>.ErrorResult(new Error(ProblemType.InvalidObjectName));
        }

        return Result<ObjectName>.SuccessResult(new ObjectName(value));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static bool operator ==(ObjectName? right, string? left) => right?.Value == left;
    
    public static bool operator !=(ObjectName? right, string? left) => right?.Value != left;
}