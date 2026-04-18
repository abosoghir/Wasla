namespace Wasla.Features.AuthenticationManagement.ResetPassword;

public record ResetPasswordRequest(
    string Email, // we take this from forget password endpoint response 
    string Code,
    string NewPassword
) : IRequest<Result>;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Your password is too weak. Please use at least 8 characters including uppercase, lowercase, a number, and a special character.");
    }
}