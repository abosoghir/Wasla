using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Profile;

namespace Wasla.Persistence.EntitiesConfigurations;

public class HelperServiceConfiguration : IEntityTypeConfiguration<HelperService>
{
    public void Configure(EntityTypeBuilder<HelperService> builder)
    {
        builder.HasKey(hs => hs.Id);

        builder.Property(hs => hs.HelperId)
            .IsRequired();

        builder.Property(hs => hs.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(hs => hs.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(hs => hs.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(hs => hs.DiscountPrice)
            .HasPrecision(18, 2);

        builder.Property(hs => hs.DeliveryDays)
            .IsRequired();

        builder.Property(hs => hs.RevisionsCount)
            .IsRequired();

        builder.Property(hs => hs.Category)
            .IsRequired();

        builder.Property(hs => hs.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(hs => hs.Helper)
            .WithMany(h => h.Services)
            .HasForeignKey(hs => hs.HelperId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}