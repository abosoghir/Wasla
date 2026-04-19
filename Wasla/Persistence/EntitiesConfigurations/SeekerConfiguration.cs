using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class SeekerConfiguration : IEntityTypeConfiguration<Seeker>
{
    public void Configure(EntityTypeBuilder<Seeker> builder)
    {
       
        builder.HasKey(s => s.Id);

        builder.Property(s => s.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Location)
            .HasMaxLength(500);

        builder.Property(s => s.CompanyName)
            .HasMaxLength(200);

        builder.Property(s => s.TotalAmountSpent)
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(s => s.TotalTasksPosted)
            .HasDefaultValue(0);

        builder.Property(s => s.TotalSessionsBooked)
            .HasDefaultValue(0);

        builder.HasOne(s => s.User)
            .WithOne(u => u.Seeker)
            .HasForeignKey<Seeker>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Tasks)
            .WithOne(t => t.Seeker)
            .HasForeignKey(t => t.SeekerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Sessions)
            .WithOne(se => se.Seeker)
            .HasForeignKey(se => se.SeekerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Projects)
            .WithOne(p => p.Seeker)
            .HasForeignKey(p => p.SeekerId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
