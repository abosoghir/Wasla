using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class UserBadgeConfiguration : IEntityTypeConfiguration<UserBadge>
{
    public void Configure(EntityTypeBuilder<UserBadge> builder)
    {

        builder.HasKey(ub => ub.Id);


        builder.Property(ub => ub.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ub => ub.BadgeId)
            .IsRequired();

        builder.Property(ub => ub.EarnedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ub => ub.IsDisplayed)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(ub => ub.DisplayOrder)
            .IsRequired()
            .HasDefaultValue(0);

    
        builder.HasOne(ub => ub.User)
            .WithMany(u => u.UserBadges)
            .HasForeignKey(ub => ub.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ub => ub.Badge)
            .WithMany(b => b.UserBadges)
            .HasForeignKey(ub => ub.BadgeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

