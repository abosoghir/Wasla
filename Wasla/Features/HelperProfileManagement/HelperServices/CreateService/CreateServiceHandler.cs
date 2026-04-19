namespace Wasla.Features.HelperProfileManagement.HelperServices.CreateService;

public class CreateServiceHandler(
    IRepository<Helper> helperRepo,
    IRepository<HelperService> serviceRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateServiceRequest, Result<CreateServiceResponse>>
{
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<HelperService> _serviceRepo = serviceRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateServiceResponse>> Handle(CreateServiceRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);

        if (helper is null)
            return Result.Failure<CreateServiceResponse>(HelperProfileErrors.HelperNotFound);

        var service = new HelperService
        {
            HelperId = helper.Id,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            DiscountPrice = request.DiscountPrice,
            DeliveryDays = request.DeliveryDays,
            RevisionsCount = request.RevisionsCount,
            Category = request.Category,
            IsActive = true,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _serviceRepo.AddAsync(service, ct);
        await _serviceRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateServiceResponse(service.Id));
    }
}
