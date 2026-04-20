namespace Wasla.Features.Marketplace.TaskOffers.CreateOffer;

public record CreateOfferRequest(int TaskId, string? Message, decimal ProposedPrice, int ProposedDurationDays) : IRequest<Result<CreateOfferResponse>>;

public record CreateOfferResponse(int Id);

public class CreateOfferRequestValidator : AbstractValidator<CreateOfferRequest>
{
    public CreateOfferRequestValidator()
    {
        RuleFor(x => x.TaskId).GreaterThan(0);
        RuleFor(x => x.ProposedPrice).GreaterThan(0);
        RuleFor(x => x.ProposedDurationDays).GreaterThan(0);
        RuleFor(x => x.Message).MaximumLength(2000).When(x => x.Message is not null);
    }
}
