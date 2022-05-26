using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

// Virtual members exist for EF lazy loading
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class Folder
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private Folder()
#pragma warning restore CS8618
    {
        AccessGroupRights = Array.Empty<FolderAccessGroupRight>();
        Children = Array.Empty<Folder>();
        Parent = null!;
    }
    
    public Folder(
        Guid id,
        ObjectName name,
        FolderDescription description)
        : this()
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
    public Guid Id { get; private set; }
    
    public ObjectName Name { get; private set; }

    public FolderDescription Description { get; private set; }
 
    public virtual Folder? Parent { get; private set; }

    public virtual ICollection<Folder> Children { get; private set; }

    public virtual ICollection<FolderAccessGroupRight> AccessGroupRights { get; private set; }
}