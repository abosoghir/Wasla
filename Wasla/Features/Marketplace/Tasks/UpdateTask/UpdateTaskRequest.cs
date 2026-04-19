namespace Wasla.Features.Marketplace.Tasks.UpdateTask;

public record UpdateTaskRequest(int Id, string Title, string Description, TaskCategory Category, decimal? Budget, DateTime? Deadline) : IRequest<Result>;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(5000);
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.Budget).GreaterThan(0).When(x => x.Budget.HasValue);
    }
}
