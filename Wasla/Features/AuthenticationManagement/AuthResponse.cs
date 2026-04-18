namespace Wasla.Features.AuthenticationManagement;

public record AuthResponse(
    string Id,
    string Email,
    string Name,
    string? PhoneNumber,
    IList<string> Roles,
    string Token,
    int ExpirseIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);