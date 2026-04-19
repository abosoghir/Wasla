using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Gamification;

namespace Wasla.Persistence.EntitiesConfigurations;

public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).HasMaxLength(100);
        builder.Property(b => b.Description).HasMaxLength(500);
        builder.Property(b => b.IconUrl).HasMaxLength(500);

        builder.HasIndex(b => b.Name).IsUnique();
    }
}
