using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

// Virtual members exist for EF lazy loading
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class User
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private User()
#pragma warning restore CS8618
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

    public PersonalName Name { get; set; }

    public Email Email { get; set; }

    public Password Password { get; set; }

    public virtual ICollection<AccessGroup> Access { get; private set; }
}