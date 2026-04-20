namespace Wasla.Features.Marketplace.TaskOffers.RejectOffer;

public record RejectOfferRequest(int Id) : IRequest<Result>;
