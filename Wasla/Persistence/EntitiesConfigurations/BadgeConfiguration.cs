using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Gamification;

namespace Wasla.Persistence.EntitiesConfigurations;

public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(b => b.IconUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(b => b.DisplayOrder)
            .IsRequired();

        builder.Property(b => b.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasMany(b => b.UserBadges)
            .WithOne(ub => ub.Badge)
            .HasForeignKey(ub => ub.BadgeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
