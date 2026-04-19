using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SeekerId)
            .IsRequired();

        builder.Property(s => s.HelperId)
            .IsRequired();

        builder.Property(s => s.ScheduledAt)
            .IsRequired();

        builder.Property(s => s.DurationMinutes)
            .IsRequired();

        builder.Property(s => s.TotalPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(s => s.MeetingLink)
            .HasMaxLength(500);

        builder.Property(s => s.MeetingPlatform)
            .HasMaxLength(50);

        builder.Property(s => s.Notes)
            .HasMaxLength(1000);

        builder.Property(s => s.IsFreeSession)
            .HasDefaultValue(false);

        builder.Property(s => s.Status)
            .HasConversion<int>();

        builder.HasOne(s => s.Seeker)
            .WithMany(sk => sk.Sessions)
            .HasForeignKey(s => s.SeekerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Helper)
            .WithMany(h => h.Sessions)
            .HasForeignKey(s => s.HelperId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Messages)
            .WithOne(m => m.Session)
            .HasForeignKey(m => m.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
