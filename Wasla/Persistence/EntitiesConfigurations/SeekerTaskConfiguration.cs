using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class SeekerTaskConfiguration : IEntityTypeConfiguration<SeekerTask>
{
    public void Configure(EntityTypeBuilder<SeekerTask> builder)
    {
        
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(t => t.PointsAwarded)
            .HasDefaultValue(10);

        builder.Property(t => t.IsFreeTask)
            .HasDefaultValue(false);


        builder.Property(t => t.Budget)
            .HasPrecision(18, 2);

        builder.Property(t => t.FinalPrice)
            .HasPrecision(18, 2);

        builder.Property(t => t.PlatformFee)
            .HasPrecision(5, 2) 
            .HasDefaultValue(0.05m);

        builder.Property(t => t.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(t => t.Category)
            .HasConversion<int>()
            .IsRequired();


        builder.HasOne(t => t.Seeker)
           .WithMany(s => s.Tasks)
           .HasForeignKey(t => t.SeekerId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Helper)
            .WithMany()
            .HasForeignKey(t => t.HelperId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(t => t.Offers)
            .WithOne(o => o.Task)
            .HasForeignKey(o => o.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Messages)
            .WithOne(m => m.Task)
            .HasForeignKey(m => m.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Attachments)
            .WithOne(a => a.Task)
            .HasForeignKey(a => a.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.CompletedAt);

        builder.Property(t => t.Deadline);

    }
}