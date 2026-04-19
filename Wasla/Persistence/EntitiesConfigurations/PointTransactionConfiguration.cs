using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Gamification;

namespace Wasla.Persistence.EntitiesConfigurations;

public class PointTransactionConfiguration : IEntityTypeConfiguration<PointTransaction>
{
    public void Configure(EntityTypeBuilder<PointTransaction> builder)
    {
        builder.HasKey(pt => pt.Id);


        builder.Property(pt => pt.RelatedEntityType).HasMaxLength(50);
        builder.Property(pt => pt.Description).HasMaxLength(500);

        builder.HasOne(pt => pt.Helper)
            .WithMany(h => h.PointTransactions)
            .HasForeignKey(pt => pt.HelperId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(pt => pt.HelperId);
        builder.HasIndex(pt => pt.Type);
    }
}
