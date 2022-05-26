using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

public class FolderAccessGroupRight
{
    private FolderAccessGroupRight()
    {
        AccessRights = Array.Empty<AccessRight>();
    }
    
    public FolderAccessGroupRight(
        Folder folder,
        AccessGroup accessGroup)
        : this()
    {
        Folder = folder;
        AccessGroup = accessGroup;
    }
    
    public virtual Folder Folder { get; private set; }

    public virtual AccessGroup AccessGroup { get; private set; }

    public ICollection<AccessRight> AccessRights { get; private set; }
}