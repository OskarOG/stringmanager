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

    public Folder Folder { get; private set; }

    public AccessGroup AccessGroup { get; private set; }

    public ICollection<AccessRight> AccessRights { get; private set; }
}