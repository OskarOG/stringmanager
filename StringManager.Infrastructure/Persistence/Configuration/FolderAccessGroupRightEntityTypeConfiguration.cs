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
            .Metadata.SetValueComparer(new ValueComparer<ICollection<AccessRightType>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));
    }

    private static string MapAccessRightListToString(ICollection<AccessRightType> accessRights)
    {
        var accessStringBuilder = new StringBuilder();
        for (var i = 0; i < 4; i++)
        {
            accessStringBuilder.Append(
                GetAccessRightChar(i, accessRights));
        }

        return accessStringBuilder.ToString();
    }

    private static char GetAccessRightChar(int pos, ICollection<AccessRightType> accessRights) =>
        pos switch
        {
            CreatePos => accessRights.Contains(AccessRightType.Create) ? CreateChar : DeniedChar,
            ReadPos => accessRights.Contains(AccessRightType.Read) ? ReadChar : DeniedChar,
            UpdatePos => accessRights.Contains(AccessRightType.Update) ? UpdateChar : DeniedChar,
            DeletePos => accessRights.Contains(AccessRightType.Delete) ? DeleteChar : DeniedChar,
            _ => DeniedChar
        };
    
    private static IEnumerable<AccessRightType> MapStringToAccessRightList(string s)
    {
        if (s[CreatePos] == CreateChar)
        {
            yield return AccessRightType.Create;
        }

        if (s[ReadPos] == ReadChar)
        {
            yield return AccessRightType.Read;
        }

        if (s[UpdatePos] == UpdateChar)
        {
            yield return AccessRightType.Update;
        }

        if (s[DeletePos] == DeleteChar)
        {
            yield return AccessRightType.Delete;
        }
    }
}