using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();


        builder.HasMany(s => s.HelperSkills)
            .WithOne(hs => hs.Skill)
            .HasForeignKey(hs => hs.SkillId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.ProjectSkills)
            .WithOne(ps => ps.Skill)
            .HasForeignKey(ps => ps.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
