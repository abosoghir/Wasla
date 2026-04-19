using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class ProjectOfferConfiguration : IEntityTypeConfiguration<ProjectOffer>
{
    public void Configure(EntityTypeBuilder<ProjectOffer> builder)
    {
        
        builder.HasKey(po => po.Id);

        
        builder.Property(po => po.Message)
            .HasMaxLength(1000);

        builder.Property(po => po.ProposedPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(po => po.ProposedDurationDays)
            .IsRequired();

        builder.Property(po => po.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(po => po.HelperId)
            .IsRequired();

        builder.Property(po => po.ProjectId)
            .IsRequired();

        builder.Property(po => po.AcceptedAt);

        builder.Property(po => po.ExpiresAt);

        builder.HasOne(po => po.Project)
            .WithMany(p => p.Offers)
            .HasForeignKey(po => po.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(po => po.Helper)
            .WithMany(h => h.ProjectOffers) 
            .HasForeignKey(po => po.HelperId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}