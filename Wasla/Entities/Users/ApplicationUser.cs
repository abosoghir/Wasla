using Microsoft.AspNetCore.Identity;

namespace EduBrain.Entities.Users;

public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Id = Guid.CreateVersion7().ToString();
        SecurityStamp = Guid.CreateVersion7().ToString();
    }
    public string Name { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }

    public string NationalId { get; set; } = string.Empty;

    public List<RefreshToken> RefreshTokens { get; set; } = [];


}
