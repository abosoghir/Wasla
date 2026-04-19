using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class ServicePackageConfiguration : IEntityTypeConfiguration<ServicePackage>
{
    public void Configure(EntityTypeBuilder<ServicePackage> builder)
    {

        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.ServiceId)
            .IsRequired();

        builder.Property(sp => sp.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(sp => sp.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(sp => sp.DeliveryDays)
            .IsRequired();

        builder.Property(sp => sp.RevisionsCount)
            .HasDefaultValue(0);

        builder.Property(sp => sp.Price)
            .HasPrecision(18, 2)
            .IsRequired();

 
        builder.Property(sp => sp.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(sp => sp.Service)
            .WithMany(hs => hs.Packages)
            .HasForeignKey(sp => sp.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
