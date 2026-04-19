using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.TrustSafety;

namespace Wasla.Persistence.EntitiesConfigurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.UserId).HasMaxLength(100);
        builder.Property(f => f.Notes).HasMaxLength(500);

        builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(f => f.UserId);
        builder.HasIndex(f => new { f.UserId, f.Type, f.EntityId }).IsUnique();
    }
}
