namespace Wasla.Features.Marketplace.Tasks.CompleteTask;

public record CompleteTaskRequest(int Id) : IRequest<Result>;
