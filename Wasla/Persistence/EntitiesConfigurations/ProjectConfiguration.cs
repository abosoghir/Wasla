using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {

        builder.HasKey(p => p.Id);


        builder.Property(p => p.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(p => p.RequiredSkills)
            .HasMaxLength(1000);

        builder.Property(p => p.DurationDays)
            .IsRequired();

        builder.Property(p => p.IsPublic)
            .HasDefaultValue(true);

        builder.Property(p => p.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(p => p.Category)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(p => p.Budget)
            .HasPrecision(18, 2)
            .IsRequired();


        builder.HasOne(p => p.Seeker)
            .WithMany(s => s.Projects)
            .HasForeignKey(p => p.SeekerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Milestones)
            .WithOne(m => m.Project)
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Offers)
            .WithOne(o => o.Project)
            .HasForeignKey(o => o.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Attachments)
            .WithOne(a => a.Project)
            .HasForeignKey(a => a.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

