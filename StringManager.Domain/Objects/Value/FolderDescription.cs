namespace StringManager.Domain.Objects.Value;

public class FolderDescription : ValueObject 
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private FolderDescription() {}
#pragma warning restore CS8618
    
    public FolderDescription(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}