using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

public class User
{
    private User()
    {
        Access = Array.Empty<AccessGroup>();
    }
    
    public User(
        Guid id,
        PersonalName name,
        Email email,
        Password password)
        : this()
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    public Guid Id { get; private set; }

    public PersonalName Name { get; private set; }

    public Email Email { get; private set; }

    public Password Password { get; private set; }

    public virtual ICollection<AccessGroup> Access { get; private set; }
}