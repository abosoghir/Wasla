using EduBrain.Entities.Users;

namespace EduBrain.Common.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn, string jti) GenerateToken(ApplicationUser user, IEnumerable<string> roles);

    string? ValidateExpiredToken(string token);
}
