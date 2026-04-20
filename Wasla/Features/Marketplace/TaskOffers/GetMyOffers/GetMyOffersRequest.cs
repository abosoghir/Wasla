namespace Wasla.Features.Marketplace.TaskOffers.GetMyOffers;

public record GetMyOffersRequest(TaskOfferStatus? Status, int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<GetMyOffersResponse>>;

public record GetMyOffersResponse(List<MyOfferResponse> Items, PaginationResponse Pagination);

public record MyOfferResponse(
    int Id,
    int TaskId,
    string TaskTitle,
    TaskCategory TaskCategory,
    string? Message,
    decimal ProposedPrice,
    int ProposedDurationDays,
    TaskOfferStatus Status,
    DateTime CreatedOn
);
