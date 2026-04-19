using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Identity;

namespace Wasla.Persistence.EntitiesConfigurations;

public class HelperConfiguration : IEntityTypeConfiguration<Helper>
{
    public void Configure(EntityTypeBuilder<Helper> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.UserId).HasMaxLength(100);
        builder.Property(h => h.Headline).HasMaxLength(200);
        builder.Property(h => h.Location).HasMaxLength(200);
        builder.Property(h => h.HourlyRate).HasPrecision(18, 2);
        builder.Property(h => h.TotalEarnings).HasPrecision(18, 2);

        builder.HasOne(h => h.User)
            .WithOne(u => u.Helper)
            .HasForeignKey<Helper>(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(h => h.UserId).IsUnique();
        builder.HasIndex(h => h.IsAvailable);
        builder.HasIndex(h => h.IsVerified);
        builder.HasIndex(h => h.AverageRating);
    }
}
