using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Identity;

namespace Wasla.Persistence.EntitiesConfigurations;

public class HelperConfiguration : IEntityTypeConfiguration<Helper>
{
    public void Configure(EntityTypeBuilder<Helper> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(h => h.Headline)
            .HasMaxLength(200);

        builder.Property(h => h.Location)
            .HasMaxLength(200);

        builder.Property(h => h.HourlyRate)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(h => h.TotalEarnings)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(h => h.IsAvailable)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(h => h.IsVerified)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(h => h.Points)
            .IsRequired();

        builder.Property(h => h.LifetimePoints)
            .IsRequired();

        builder.Property(h => h.CompletedTasksCount)
            .IsRequired();

        builder.Property(h => h.CompletedSessionsCount)
            .IsRequired();

        builder.Property(h => h.CompletedProjectsCount)
            .IsRequired();

        builder.Property(h => h.SpeedOfResponseInMintues)
            .IsRequired();

        builder.Property(h => h.AverageRating)
            .IsRequired();

        builder.Property(h => h.TotalReviewsCount)
            .IsRequired();

        builder.Ignore(h => h.IsNextTaskFree);

        builder.HasOne(h => h.User)
            .WithOne(u => u.Helper)
            .HasForeignKey<Helper>(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
