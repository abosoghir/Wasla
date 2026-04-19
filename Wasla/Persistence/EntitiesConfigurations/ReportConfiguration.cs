using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {

        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReporterId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.ReportedUserId)
            .HasMaxLength(100);

        builder.Property(r => r.ResolvedById)
            .HasMaxLength(100);

        builder.Property(r => r.Reason)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(r => r.Details);

        builder.Property(r => r.Resolution)
            .HasMaxLength(1000);

        builder.Property(r => r.RelatedEntityType)
            .HasMaxLength(100);

        builder.Property(r => r.EvidenceUrls)
            .HasMaxLength(2000);


        builder.Property(r => r.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.Severity)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.ResolvedAt);



        builder.HasOne(r => r.Reporter)
            .WithMany(u => u.SubmittedReports) 
            .HasForeignKey(r => r.ReporterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ReportedUser)
            .WithMany(u => u.ReportsAgainstMe)
            .HasForeignKey(r => r.ReportedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ResolvedBy)
            .WithMany(u => u.ResolvedReports)
            .HasForeignKey(r => r.ResolvedById)
            .OnDelete(DeleteBehavior.SetNull);

    }
}
