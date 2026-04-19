using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WaslaS.Persistence.EntitiesConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        //Default Data   --> UserRoles
        //builder.HasData(new IdentityUserRole<string>
        //{
        //    UserId = DefaultUsers.AdminId,
        //    RoleId = DefaultRoles.AdminRoleId
        //});

        var adminRoles = DefaultUsers.Admins.Select(a => new IdentityUserRole<string>
        {
            UserId = a.Id,
            RoleId = DefaultRoles.AdminRoleId
        }).ToArray();

        builder.HasData(adminRoles);

    }
}