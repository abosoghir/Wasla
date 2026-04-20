namespace Wasla.Features.HelperBrowse.GetHelperById;

public record GetHelperByIdRequest(int Id) : IRequest<Result<GetHelperByIdResponse>>;