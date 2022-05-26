using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

public class AccessGroup
{
    private AccessGroup()
    {
        Users = Array.Empty<User>();
        AccessibleFolders = Array.Empty<FolderAccessGroupRight>();
        Children = Array.Empty<AccessGroup>();
    }
    
    public AccessGroup(
        Guid id,
        ObjectName name,
        AccessGroup? parent)
        : this()
    {
        Id = id;
        Name = name;
        Parent = parent;
    }

    public Guid Id { get; private set; }

    public ObjectName Name { get; private set; }

    public virtual AccessGroup? Parent { get; private set; }

    public virtual ICollection<AccessGroup> Children { get; private set; }

    public virtual ICollection<User> Users { get; private set; }

    public virtual ICollection<FolderAccessGroupRight> AccessibleFolders { get; private set; }
}