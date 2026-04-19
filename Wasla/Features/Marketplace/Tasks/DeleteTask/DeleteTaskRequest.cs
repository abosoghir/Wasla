namespace Wasla.Features.Marketplace.Tasks.DeleteTask;

public record DeleteTaskRequest(int Id) : IRequest<Result>;
