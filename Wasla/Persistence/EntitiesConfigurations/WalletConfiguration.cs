using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {

        builder.HasKey(w => w.Id);

        builder.Property(w => w.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(w => w.Currency)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(CurrencyType.EGP);

        builder.Property(w => w.Balance)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(w => w.TotalDeposited)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(w => w.TotalWithdrawn)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(w => w.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(w => w.User)
            .WithMany(u => u.Wallets)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Transactions)
            .WithOne(t => t.Wallet)
            .HasForeignKey(t => t.WalletId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
