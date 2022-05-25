using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringManager.Domain.Objects.Entity;

namespace StringManager.Infrastructure.Persistence.Configuration;

public class AccessGroupEntityTypeConfiguration : IEntityTypeConfiguration<AccessGroup>
{
    public void Configure(EntityTypeBuilder<AccessGroup> builder)
    {
        builder
            .ToTable($"{nameof(AccessGroup)}")
            .HasKey(x => x.Id);

        builder
            .OwnsOne(x => x.Name)
            .Property(p => p.Value)
            .HasColumnName("Name");

        builder
            .HasOne(p => p.Parent)
            .WithMany(p => p.Children);

        builder
            .HasMany(p => p.Users)
            .WithMany(p => p.Access);

        builder
            .HasMany(p => p.AccessibleFolders)
            .WithOne(p => p.AccessGroup);
    }
}