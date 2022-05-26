using StringManager.Domain.Objects.Value;

namespace StringManager.Domain.Objects.Entity;

// Virtual members exist for EF lazy loading
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class FolderAccessGroupRight
{
    // Constructor for EF initialization to backing fields
#pragma warning disable CS8618
    private FolderAccessGroupRight()
#pragma warning restore CS8618
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