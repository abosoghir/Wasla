namespace Wasla.Features.Marketplace.Tasks.GetTaskById;

public class GetTaskByIdHandler(IRepository<SeekerTask> taskRepo)
    : IRequestHandler<GetTaskByIdRequest, Result<GetTaskByIdResponse>>
{
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;

    public async Task<Result<GetTaskByIdResponse>> Handle(GetTaskByIdRequest request, CancellationToken ct)
    {
        var task = await _taskRepo
            .Include(t => t.Offers)
            .Where(t => t.Id == request.Id && !t.IsDeleted)
            .Select(t => new GetTaskByIdResponse(
                t.Id, t.SeekerId, t.HelperId, t.Title, t.Description,
                t.Category, t.Status, t.Budget, t.Deadline,
                t.CompletedAt, t.CreatedOn,
                t.Offers.Select(o => new TaskOfferResponse(
                    o.Id, o.HelperId, o.Message, o.ProposedPrice, o.ProposedDurationDays, o.Status
                )).ToList()
            ))
            .FirstOrDefaultAsync(ct);

        if (task is null)
            return Result.Failure<GetTaskByIdResponse>(MarketplaceErrors.TaskNotFound);

        return Result.Success(task);
    }
}
