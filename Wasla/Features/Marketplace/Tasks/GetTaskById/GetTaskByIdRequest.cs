namespace Wasla.Features.Marketplace.Tasks.GetTaskById;

public record GetTaskByIdRequest(int Id) : IRequest<Result<GetTaskByIdResponse>>;

public record GetTaskByIdResponse(
    int Id, int SeekerId, int? HelperId, string Title, string Description,
    TaskCategory Category, Common.Enums.TaskStatus Status, decimal? Budget,
    DateTime? Deadline, DateTime? CompletedAt, DateTime CreatedOn,
    List<TaskOfferResponse> Offers
);

public record TaskOfferResponse(int Id, int HelperId, string? Message, decimal ProposedPrice, int ProposedDurationDays, TaskOfferStatus Status);
