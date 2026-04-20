namespace Wasla.Features.AuthenticationManagement.ChangePassword;

public record ChangePasswordRequest(string OldPassword, string NewPassword, string ConfirmNewPassword) : IRequest<Result>;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.OldPassword).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
        RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
    }
}
