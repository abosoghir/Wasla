using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        //Default Data
        builder.HasData([
            new ApplicationRole
            {
                Id = DefaultRoles.AdminRoleId,
                Name = DefaultRoles.Admin,
                NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp
            },
            new ApplicationRole
            {
                Id = DefaultRoles.SeekerRoleId,
                Name = DefaultRoles.Seeker,
                NormalizedName = DefaultRoles.Seeker.ToUpper(),
                ConcurrencyStamp = DefaultRoles.SeekerRoleConcurrencyStamp,
                IsDefault = true
            },
            new ApplicationRole
            {
                Id = DefaultRoles.HelperRoleId,
                Name = DefaultRoles.Helper,
                NormalizedName = DefaultRoles.Helper.ToUpper(),
                ConcurrencyStamp = DefaultRoles.HelperRoleConcurrencyStamp,
                IsDefault = true
            }
        ]);

    }
}
