using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Marketplace;

namespace Wasla.Persistence.EntitiesConfigurations;

public class ProjectAttachmentConfiguration : IEntityTypeConfiguration<ProjectAttachment>
{
    public void Configure(EntityTypeBuilder<ProjectAttachment> builder)
    {
        builder.HasKey(pa => pa.Id);
        
        builder.Property(pa => pa.FileName).HasMaxLength(200);
        builder.Property(pa => pa.FileUrl).HasMaxLength(500);
        builder.Property(pa => pa.FileType).HasMaxLength(50);
        builder.Property(pa => pa.Description).HasMaxLength(1000);

        builder.HasOne(pa => pa.Project)
            .WithMany(p => p.Attachments)
            .HasForeignKey(pa => pa.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
