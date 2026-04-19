using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Communication;

namespace Wasla.Persistence.EntitiesConfigurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.UserId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(n => n.Type)
            .IsRequired();

        builder.Property(n => n.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(n => n.Body)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(n => n.ActionUrl)
            .HasMaxLength(500);

        builder.Property(n => n.RelatedEntityType)
            .HasMaxLength(50);

        builder.Property(n => n.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(n => n.Priority)
            .IsRequired();

        builder.Property(n => n.Channel)
            .IsRequired();

        builder.Property(n => n.IsSent)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}