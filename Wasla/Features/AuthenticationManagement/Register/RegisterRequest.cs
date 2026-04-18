namespace Wasla.Features.AuthenticationManagement.Register;

public record RegisterRequest(
    string Name,
    string Email,
    string PhoneNumber,
    string Password
) : IRequest<Result<RegisterResponse>>;


public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);   // you can use MustAsync with a custom email validation method if needed

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Your password is too weak. It must be at least 6 characters long and include a number and a special character.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(x => x.PhoneNumber)
           .NotEmpty()
           .MaximumLength(20)
           .Matches(RegexPatterns.PhoneNumber)
           .WithMessage("Invalid phone number. It should be 11 digits and start with 010, 011, 012, or 015.");

    }
}