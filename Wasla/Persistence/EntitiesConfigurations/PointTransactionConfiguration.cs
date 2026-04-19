using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Gamification;

namespace Wasla.Persistence.EntitiesConfigurations;

public class PointTransactionConfiguration : IEntityTypeConfiguration<PointTransaction>
{
    public void Configure(EntityTypeBuilder<PointTransaction> builder)
    {
        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.HelperId)
            .IsRequired();

        builder.Property(pt => pt.Type)
            .IsRequired();

        builder.Property(pt => pt.Points)
            .IsRequired();

        builder.Property(pt => pt.BalanceAfter)
            .IsRequired();

        builder.Property(pt => pt.RelatedEntityType)
            .HasMaxLength(50);

        builder.Property(pt => pt.Description)
            .HasMaxLength(500);

        builder.Property(pt => pt.RelatedEntityId);

        builder.HasOne(pt => pt.Helper)
            .WithMany(h => h.PointTransactions)
            .HasForeignKey(pt => pt.HelperId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}