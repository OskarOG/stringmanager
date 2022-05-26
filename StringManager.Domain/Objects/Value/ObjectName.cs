namespace StringManager.Domain.Objects.Value;

public class ObjectName : ValueObject
{
    private ObjectName()
    {
    }
    
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