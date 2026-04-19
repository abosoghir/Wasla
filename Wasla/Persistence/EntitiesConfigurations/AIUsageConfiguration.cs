using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.AI;

namespace Wasla.Persistence.EntitiesConfigurations;

public class AIUsageConfiguration : IEntityTypeConfiguration<AIUsage>
{
    public void Configure(EntityTypeBuilder<AIUsage> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.UserId).HasMaxLength(100);
        builder.Property(a => a.ErrorMessage).HasMaxLength(500);

        builder.HasOne(a => a.User)
            .WithMany(u => u.AIUsages)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.PointTransaction)
            .WithMany()
            .HasForeignKey(a => a.PointTransactionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.UsedAt);
    }
}
