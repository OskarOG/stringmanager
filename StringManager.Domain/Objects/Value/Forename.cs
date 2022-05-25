namespace StringManager.Domain.Objects.Value;

public class Forename : ValueObject
{
    public Forename(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}