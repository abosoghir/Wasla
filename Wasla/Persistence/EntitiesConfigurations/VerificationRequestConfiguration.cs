using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class VerificationRequestConfiguration : IEntityTypeConfiguration<VerificationRequest>
{
    public void Configure(EntityTypeBuilder<VerificationRequest> builder)
    {

        builder.HasKey(v => v.Id);

        builder.Property(v => v.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(v => v.Status)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(VerificationStatus.Pending);

        builder.Property(v => v.DocumentUrl)
            .HasMaxLength(500);

        builder.Property(v => v.DocumentNumber)
            .HasMaxLength(100);

        builder.Property(v => v.FullName)
            .HasMaxLength(200);

        builder.Property(v => v.Address)
            .HasMaxLength(500);

        builder.Property(v => v.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(v => v.Notes)
            .HasMaxLength(1000);

        builder.Property(v => v.RejectionReason)
            .HasMaxLength(500);

        builder.Property(v => v.ReviewedById)
            .HasMaxLength(100);

        builder.Property(v => v.ReviewedAt);

        builder.Property(v => v.DateOfBirth);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.ReviewedBy)
            .WithMany()
            .HasForeignKey(v => v.ReviewedById)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
