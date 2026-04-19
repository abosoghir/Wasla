using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wasla.Persistence.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");


        builder.Property(x => x.Id).HasMaxLength(100);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);

        builder.Property(x => x.EmailConfirmationCode).HasMaxLength(10);
        builder.Property(x => x.ResetPasswordCode).HasMaxLength(10);

        builder.Property(x => x.Email).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.UserName).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.NormalizedEmail).HasMaxLength(200).IsUnicode(false);
        builder.Property(x => x.NormalizedUserName).HasMaxLength(200).IsUnicode(false);


        //Default Data

        var passwordHasher = new PasswordHasher<ApplicationUser>();
       

        var adminUsers = DefaultUsers.Admins.Select(a => new ApplicationUser
        {
            Id = a.Id,
            Name = "Admin",
            UserName = a.Email,
            NormalizedUserName = a.Email.ToUpper(),
            Email = a.Email,
            NormalizedEmail = a.Email.ToUpper(),
            SecurityStamp = a.SecurityStamp,
            ConcurrencyStamp = a.ConcurrencyStamp,
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null!, a.Password)
        }).ToArray();

        builder.HasData(adminUsers);
    }
}
