namespace StringManager.Domain.Objects.Value;

public class PersonalName : ValueObject
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private PersonalName() {}
#pragma warning restore CS8618
    
    public PersonalName(
        string forename,
        string surname)
    {
        Forename = forename;
        Surname = surname;
    }

    public string Forename { get; private set; }

    public string Surname { get; private set; }

    public string FullName => $"{Forename} {Surname}";
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Forename;
        yield return Surname;
    }
}