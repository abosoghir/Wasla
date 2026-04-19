using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Marketplace;

namespace Wasla.Persistence.EntitiesConfigurations;

public class TaskAttachmentConfiguration : IEntityTypeConfiguration<TaskAttachment>
{
    public void Configure(EntityTypeBuilder<TaskAttachment> builder)
    {
        builder.HasKey(ta => ta.Id);
        
        builder.Property(ta => ta.FileName).HasMaxLength(200);
        builder.Property(ta => ta.FileUrl).HasMaxLength(500);
        builder.Property(ta => ta.FileType).HasMaxLength(50);
        builder.Property(ta => ta.UploadedById).HasMaxLength(100);

        builder.HasOne(ta => ta.Task)
            .WithMany(t => t.Attachments)
            .HasForeignKey(ta => ta.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
