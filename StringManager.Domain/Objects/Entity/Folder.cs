using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

public class Folder
{
    private Folder()
    {
        AccessGroupRights = Array.Empty<FolderAccessGroupRight>();
        Children = Array.Empty<Folder>();
    }
    
    public Folder(
        Guid id,
        ObjectName name,
        FolderDescription description,
        Folder? parent)
        : this()
    {
        Id = id;
        Name = name;
        Description = description;
        Parent = parent;
    }
    
    public Guid Id { get; private set; }
    
    public ObjectName Name { get; private set; }

    public FolderDescription Description { get; private set; }
 
    public virtual Folder? Parent { get; private set; }

    public virtual ICollection<Folder> Children { get; private set; }

    public virtual ICollection<FolderAccessGroupRight> AccessGroupRights { get; private set; }
}