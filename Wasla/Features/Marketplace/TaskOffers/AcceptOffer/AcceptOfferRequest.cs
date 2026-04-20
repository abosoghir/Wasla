namespace Wasla.Features.Marketplace.TaskOffers.AcceptOffer;

public record AcceptOfferRequest(int Id) : IRequest<Result>;
