using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Marketplace;

namespace Wasla.Persistence.EntitiesConfigurations;

public class MilestoneDeliverableConfiguration : IEntityTypeConfiguration<MilestoneDeliverable>
{
    public void Configure(EntityTypeBuilder<MilestoneDeliverable> builder)
    {
        builder.HasKey(md => md.Id);

        builder.Property(md => md.MilestoneId)
            .IsRequired();

        builder.Property(md => md.FileName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(md => md.FileUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(md => md.FileType)
            .HasMaxLength(50);

        builder.Property(md => md.FileSize)
            .IsRequired();

        builder.Property(md => md.Description)
            .HasMaxLength(1000);

        builder.HasOne(md => md.Milestone)
            .WithMany(pm => pm.Deliverables)
            .HasForeignKey(md => md.MilestoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
