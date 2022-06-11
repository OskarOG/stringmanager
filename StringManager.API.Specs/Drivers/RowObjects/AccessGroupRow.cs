namespace StringManager.API.Specs.Drivers.RowObjects;

public class AccessGroupRow
{
    public AccessGroupRow(Guid accessGroupId, string accessGroupName)
    {
        AccessGroupId = accessGroupId;
        AccessGroupName = accessGroupName;
    }

    public Guid AccessGroupId { get;  }

    public string AccessGroupName { get; }
}