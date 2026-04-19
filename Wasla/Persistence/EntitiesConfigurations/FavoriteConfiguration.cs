using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.TrustSafety;

namespace Wasla.Persistence.EntitiesConfigurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(f => f.Type)
            .IsRequired();

        builder.Property(f => f.EntityId)
            .IsRequired();

        builder.Property(f => f.Notes)
            .HasMaxLength(500);

        builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
