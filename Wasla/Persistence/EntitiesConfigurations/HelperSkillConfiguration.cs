using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasla.Entities.Profile;

namespace Wasla.Persistence.EntitiesConfigurations;

public class HelperSkillConfiguration : IEntityTypeConfiguration<HelperSkill>
{
    public void Configure(EntityTypeBuilder<HelperSkill> builder)
    {
        builder.HasKey(hs => hs.Id);

        builder.Property(hs => hs.HelperId)
            .IsRequired();

        builder.Property(hs => hs.SkillId)
            .IsRequired();

        builder.Property(hs => hs.Proficiency)
            .IsRequired();

        builder.HasOne(hs => hs.Helper)
            .WithMany(h => h.HelperSkills)
            .HasForeignKey(hs => hs.HelperId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(hs => hs.Skill)
            .WithMany(s => s.HelperSkills)
            .HasForeignKey(hs => hs.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}