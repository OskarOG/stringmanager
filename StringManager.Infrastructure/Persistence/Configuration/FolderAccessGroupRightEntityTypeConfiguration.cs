using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringManager.Domain.Objects.Entity;
using StringManager.Domain.Objects.Value;

namespace StringManager.Infrastructure.Persistence.Configuration;

public class FolderAccessGroupRightEntityTypeConfiguration : IEntityTypeConfiguration<FolderAccessGroupRight>
{
    private const int CreatePos = 0;
    private const int ReadPos = 1;
    private const int UpdatePos = 2;
    private const int DeletePos = 3;

    private const char CreateChar = 'c';
    private const char ReadChar = 'r';
    private const char UpdateChar = 'u';
    private const char DeleteChar = 'd';
    private const char DeniedChar = '-';

    public void Configure(EntityTypeBuilder<FolderAccessGroupRight> builder)
    {
        builder
            .ToTable($"{nameof(FolderAccessGroupRight)}")
            .HasKey("FolderId", "AccessGroupId");

        builder
            .Property(p => p.AccessRights)
            .HasConversion(
                v => MapAccessRightListToString(v),
                v => MapStringToAccessRightList(v).ToList())
            .Metadata.SetValueComparer(new ValueComparer<ICollection<AccessRight>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));
    }

    private static string MapAccessRightListToString(ICollection<AccessRight> accessRights)
    {
        var accessStringBuilder = new StringBuilder();
        for (var i = 0; i < 4; i++)
        {
            accessStringBuilder.Append(
                GetAccessRightChar(i, accessRights));
        }

        return accessStringBuilder.ToString();
    }

    private static char GetAccessRightChar(int pos, ICollection<AccessRight> accessRights) =>
        pos switch
        {
            CreatePos => accessRights.Contains(AccessRight.Create) ? CreateChar : DeniedChar,
            ReadPos => accessRights.Contains(AccessRight.Read) ? ReadChar : DeniedChar,
            UpdatePos => accessRights.Contains(AccessRight.Update) ? UpdateChar : DeniedChar,
            DeletePos => accessRights.Contains(AccessRight.Delete) ? DeleteChar : DeniedChar,
            _ => DeniedChar
        };
    
    private static IEnumerable<AccessRight> MapStringToAccessRightList(string s)
    {
        if (s[CreatePos] == CreateChar)
        {
            yield return AccessRight.Create;
        }

        if (s[ReadPos] == ReadChar)
        {
            yield return AccessRight.Read;
        }

        if (s[UpdatePos] == UpdateChar)
        {
            yield return AccessRight.Update;
        }

        if (s[DeletePos] == DeleteChar)
        {
            yield return AccessRight.Delete;
        }
    }
}