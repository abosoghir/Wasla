using Wasla.Entities.Identity;

namespace Wasla.Common.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn, string jti) GenerateToken(ApplicationUser user, IEnumerable<string> roles);

    string? ValidateExpiredToken(string token);
}
