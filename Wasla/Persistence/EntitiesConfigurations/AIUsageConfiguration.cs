using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.AI;

namespace Wasla.Persistence.EntitiesConfigurations;

public class AIUsageConfiguration : IEntityTypeConfiguration<AIUsage>
{
    public void Configure(EntityTypeBuilder<AIUsage> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.ErrorMessage)
            .HasMaxLength(500);

        builder.Property(a => a.FeatureType)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.PointsCost)
            .IsRequired();

        builder.Property(a => a.InputLength);

        builder.Property(a => a.OutputLength);

        builder.Property(a => a.UsedAt)
            .IsRequired();

        builder.HasOne(a => a.User)
            .WithMany(u => u.AIUsages)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.PointTransaction)
            .WithMany()
            .HasForeignKey(a => a.PointTransactionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
