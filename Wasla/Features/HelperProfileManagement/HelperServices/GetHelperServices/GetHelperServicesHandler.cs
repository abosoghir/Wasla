namespace Wasla.Features.HelperProfileManagement.HelperServices.GetHelperServices;

public class GetHelperServicesHandler(IRepository<HelperService> serviceRepo)
    : IRequestHandler<GetHelperServicesRequest, Result<List<HelperServiceResponse>>>
{
    private readonly IRepository<HelperService> _serviceRepo = serviceRepo;

    public async Task<Result<List<HelperServiceResponse>>> Handle(GetHelperServicesRequest request, CancellationToken ct)
    {
        var services = await _serviceRepo
            .FindAll(s => s.HelperId == request.HelperId && s.IsActive && !s.IsDeleted)
            .Select(s => new HelperServiceResponse(
                s.Id, s.Title, s.Description, s.Price,
                s.DiscountPrice, s.DeliveryDays, s.Category, s.CreatedOn
            ))
            .ToListAsync(ct);

        return Result.Success(services);
    }
}
