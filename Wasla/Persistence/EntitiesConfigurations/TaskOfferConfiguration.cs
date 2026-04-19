using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class TaskOfferConfiguration : IEntityTypeConfiguration<TaskOffer>
{
    public void Configure(EntityTypeBuilder<TaskOffer> builder)
    {

        builder.HasKey(to => to.Id);

        builder.Property(to => to.TaskId)
            .IsRequired();

        builder.Property(to => to.HelperId)
            .IsRequired();

        builder.Property(to => to.ProposedPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(to => to.ProposedDurationDays)
            .IsRequired();

        builder.Property(to => to.Message)
            .HasMaxLength(1000);

        builder.Property(to => to.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(to => to.Task)
            .WithMany(t => t.Offers)
            .HasForeignKey(to => to.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(to => to.Helper)
            .WithMany(h => h.TaskOffers)
            .HasForeignKey(to => to.HelperId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

