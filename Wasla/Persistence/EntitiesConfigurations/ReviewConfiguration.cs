using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {

        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReviewerId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.RevieweeId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(1000);

        builder.Property(r => r.RelatedEntityType)
            .HasMaxLength(50);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.QualityRating);

        builder.Property(r => r.CommunicationRating);

        builder.Property(r => r.TimelinessRating);

        builder.Property(r => r.ValueRating);

   
        builder.Property(r => r.IsVisible)
            .HasDefaultValue(true);

        builder.Property(r => r.IsVerified)
            .HasDefaultValue(false);

        builder.Property(r => r.Type)
            .HasConversion<int>()
            .IsRequired();


        builder.HasOne(r => r.Reviewer)
            .WithMany(u => u.ReviewsGiven)
            .HasForeignKey(r => r.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Reviewee)
            .WithMany(u => u.ReviewsReceived)
            .HasForeignKey(r => r.RevieweeId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
