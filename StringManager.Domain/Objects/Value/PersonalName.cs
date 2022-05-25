namespace StringManager.Domain.Objects.Value;

public class PersonalName : ValueObject
{
    public PersonalName(
        Forename forename,
        Surname surname)
    {
        Forename = forename;
        Surname = surname;
    }

    public Forename Forename { get; private set; }

    public Surname Surname { get; private set; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Forename.Value;
        yield return Surname.Value;
    }
}