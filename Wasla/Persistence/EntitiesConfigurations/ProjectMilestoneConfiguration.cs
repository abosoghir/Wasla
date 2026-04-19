using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class ProjectMilestoneConfiguration : IEntityTypeConfiguration<ProjectMilestone>
{
    public void Configure(EntityTypeBuilder<ProjectMilestone> builder)
    {

        builder.HasKey(pm => pm.Id);

        builder.Property(pm => pm.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(pm => pm.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(pm => pm.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(pm => pm.OrderIndex)
            .IsRequired();

        builder.Property(pm => pm.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(pm => pm.DueDate);

        builder.Property(pm => pm.CompletedAt);

      
        builder.HasOne(pm => pm.Project)
            .WithMany(p => p.Milestones)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pm => pm.Deliverables)
            .WithOne(d => d.Milestone)
            .HasForeignKey(d => d.MilestoneId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
