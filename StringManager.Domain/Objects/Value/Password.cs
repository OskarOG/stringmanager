namespace StringManager.Domain.Objects.Value;

public class Password : ValueObject
{
    private Password()
    {
    }
    
    private Password(string hashedValue)
    {
        HashedValue = hashedValue;
    }

    public string HashedValue { get; private set; }

    public static Password NewPassword(string password) =>
        new(BCrypt.Net.BCrypt.EnhancedHashPassword(password));

    public bool VerifyPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, HashedValue);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return HashedValue;
    }
}