namespace StringManager.Domain.Objects.Value;

public class FolderDescription : ValueObject 
{
    private FolderDescription()
    {
    }
    
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