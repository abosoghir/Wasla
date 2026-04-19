namespace Wasla.Features.HelperProfileManagement.HelperServices.DeleteService;

public record DeleteServiceRequest(int Id) : IRequest<Result>;

public class DeleteServiceRequestValidator : AbstractValidator<DeleteServiceRequest>
{
    public DeleteServiceRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
