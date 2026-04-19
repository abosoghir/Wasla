namespace Wasla.Features.Marketplace.Tasks.CreateTask;

public record CreateTaskRequest(
    string Title,
    string Description,
    TaskCategory Category,
    decimal? Budget,
    DateTime? Deadline
) : IRequest<Result<CreateTaskResponse>>;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(5000);
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.Budget).GreaterThan(0).When(x => x.Budget.HasValue);
        RuleFor(x => x.Deadline).GreaterThan(DateTime.UtcNow).When(x => x.Deadline.HasValue);
    }
}
