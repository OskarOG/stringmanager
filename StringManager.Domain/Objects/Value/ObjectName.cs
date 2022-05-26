namespace StringManager.Domain.Objects.Value;

public class ObjectName : ValueObject
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private ObjectName() {}
#pragma warning restore CS8618
    
    public ObjectName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}