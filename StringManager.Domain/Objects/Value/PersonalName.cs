namespace StringManager.Domain.Objects.Value;

public class PersonalName : ValueObject
{
    private PersonalName()
    {
    }
    
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