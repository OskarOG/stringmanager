using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringManager.Domain.Objects.Entity;

namespace StringManager.Infrastructure.Persistence.Configuration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable($"{nameof(User)}")
            .HasKey(p => p.Id);

        builder
            .OwnsOne(
                p => p.Name,
                navigationBuilder =>
                {
                    navigationBuilder
                        .OwnsOne(p => p.Forename)
                        .Property(p => p.Value)
                        .HasColumnName("Forename");

                    navigationBuilder
                        .OwnsOne(p => p.Surname)
                        .Property(p => p.Value)
                        .HasColumnName("Surname");
                });

        builder
            .OwnsOne(p => p.Email)
            .Property(p => p.Value)
            .HasColumnName("Email");

        builder
            .OwnsOne(p => p.Password)
            .Property(p => p.HashedValue)
            .HasColumnName("HashedPassword");

        builder
            .HasMany(p => p.Access)
            .WithMany(p => p.Users);
    }
}