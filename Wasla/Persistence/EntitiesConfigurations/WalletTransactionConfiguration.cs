using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {

        builder.HasKey(wt => wt.Id);

        builder.Property(wt => wt.WalletId)
            .IsRequired();

        builder.Property(wt => wt.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(wt => wt.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(wt => wt.Status)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(TransactionStatus.Completed);

        builder.Property(wt => wt.BalanceAfter)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(wt => wt.ReferenceNumber)
            .HasMaxLength(100);

        builder.Property(wt => wt.Description)
            .HasMaxLength(500);

        builder.Property(wt => wt.RelatedEntityType)
            .HasMaxLength(50);

        builder.Property(wt => wt.RelatedEntityId);


        builder.HasOne(wt => wt.Wallet)
            .WithMany(w => w.Transactions)
            .HasForeignKey(wt => wt.WalletId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
