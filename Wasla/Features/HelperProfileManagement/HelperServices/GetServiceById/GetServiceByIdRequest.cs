namespace Wasla.Features.HelperProfileManagement.HelperServices.GetServiceById;

public record GetServiceByIdRequest(int Id) : IRequest<Result<GetServiceByIdResponse>>;
