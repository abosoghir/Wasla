using System.Security.Claims;

namespace EduBrain.Common.Consts;

public static class UserExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user) =>
    user.FindFirstValue(ClaimTypes.NameIdentifier);

    public static List<string> GetRoles(this ClaimsPrincipal user) =>
    user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

    public static bool IsInRole(this ClaimsPrincipal user, string role) =>
    user.GetRoles().Contains(role, StringComparer.OrdinalIgnoreCase);

    public static bool IsAdmin(this ClaimsPrincipal user) =>
    user.IsInRole(DefaultRoles.Admin);

    // تتحقق إذا كان User لديه أي Role من مجموعة
    public static bool HasAnyRole(this ClaimsPrincipal user, params string[] roles) =>
        user.GetRoles().Any(r => roles.Contains(r, StringComparer.OrdinalIgnoreCase));
}
