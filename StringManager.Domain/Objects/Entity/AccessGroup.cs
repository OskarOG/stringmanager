using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

// Virtual members exist for EF lazy loading
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class AccessGroup
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private AccessGroup()
#pragma warning restore CS8618
    {
        Users = Array.Empty<User>();
        AccessibleFolders = Array.Empty<FolderAccessGroupRight>();
        Children = Array.Empty<AccessGroup>();

        Parent = null;
    }
    
    public AccessGroup(
        Guid id,
        ObjectName name)
        : this()
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }

    public ObjectName Name { get; set; }

    public virtual AccessGroup? Parent { get; private set; }

    public virtual ICollection<AccessGroup> Children { get; private set; }

    public virtual ICollection<User> Users { get; private set; }

    public virtual ICollection<FolderAccessGroupRight> AccessibleFolders { get; private set; }
}