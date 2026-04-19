using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Communication;

namespace Wasla.Persistence.EntitiesConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.SenderId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.ReceiverId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Type)
            .IsRequired();

        builder.Property(m => m.Content)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(m => m.AttachmentUrl)
            .HasMaxLength(500);

        builder.Property(m => m.FileName)
            .HasMaxLength(200);

        builder.Property(m => m.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(m => m.IsDeletedBySender)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(m => m.IsDeletedByReceiver)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.ReplyTo)
            .WithMany(m => m.Replies)
            .HasForeignKey(m => m.ReplyToId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(m => m.Task)
            .WithMany(t => t.Messages)
            .HasForeignKey(m => m.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Project)
            .WithMany()
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Session)
            .WithMany(s => s.Messages)
            .HasForeignKey(m => m.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
