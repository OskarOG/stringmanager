using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringManager.Domain.Objects.Entity;

namespace StringManager.Infrastructure.Persistence.Configuration;

public class FolderEntityTypeConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder
            .ToTable($"{nameof(Folder)}")
            .HasKey(p => p.Id);

        builder
            .OwnsOne(p => p.Name)
            .Property(p => p.Value)
            .HasColumnName("Name");

        builder
            .OwnsOne(p => p.Description)
            .Property(p => p.Value)
            .HasColumnName("Description");

        builder
            .HasOne(p => p.Parent)
            .WithMany(p => p.Children);

        builder
            .HasMany(p => p.AccessGroupRights)
            .WithOne(p => p.Folder);
    }
}