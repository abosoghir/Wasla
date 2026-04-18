namespace Wasla.Features.AuthenticationManagement.ForgetPassword;

public record ForgetPasswordRequest(
    string Email
) : IRequest<Result<ForgetPasswordResponse>>;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress(); 
    }
}
