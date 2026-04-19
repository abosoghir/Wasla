using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Financial;

namespace Wasla.Persistence.EntitiesConfigurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PayerId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.PayeeId)
            .HasMaxLength(100);

        builder.Property(p => p.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.PlatformFee)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.Purpose)
            .IsRequired();

        builder.Property(p => p.RelatedEntityId);

        builder.Property(p => p.RelatedEntityType)
            .HasMaxLength(50);

        builder.Property(p => p.Method)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.TransactionReference)
            .HasMaxLength(100);

        builder.Property(p => p.GatewayResponse)
            .HasMaxLength(2000);

        builder.Property(p => p.PaidAt);

        builder.HasOne(p => p.Payer)
            .WithMany()
            .HasForeignKey(p => p.PayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Payee)
            .WithMany()
            .HasForeignKey(p => p.PayeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}