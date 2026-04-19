namespace Wasla.Features.HelperProfileManagement.HelperServices.GetServiceById;

public class GetServiceByIdHandler(IRepository<HelperService> serviceRepo)
    : IRequestHandler<GetServiceByIdRequest, Result<GetServiceByIdResponse>>
{
    private readonly IRepository<HelperService> _serviceRepo = serviceRepo;

    public async Task<Result<GetServiceByIdResponse>> Handle(GetServiceByIdRequest request, CancellationToken ct)
    {
        var service = await _serviceRepo
            .Include(s => s.Packages)
            .Where(s => s.Id == request.Id && !s.IsDeleted)
            .Select(s => new GetServiceByIdResponse(
                s.Id,
                s.HelperId,
                s.Title,
                s.Description,
                s.Price,
                s.DiscountPrice,
                s.DeliveryDays,
                s.RevisionsCount,
                s.Category,
                s.IsActive,
                s.CreatedOn,
                s.Packages.Select(p => new ServicePackageDto(
                    p.Id, p.Name, p.Description, p.Price,
                    p.DeliveryDays, p.RevisionsCount, p.Type
                )).ToList()
            ))
            .FirstOrDefaultAsync(ct);

        if (service is null)
            return Result.Failure<GetServiceByIdResponse>(HelperProfileErrors.ServiceNotFound);

        return Result.Success(service);
    }
}
