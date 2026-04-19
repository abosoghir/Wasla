namespace Wasla.Features.HelperProfileManagement.HelperServices.UpdateService;

public class UpdateServiceHandler(
    IRepository<HelperService> serviceRepo,
    IRepository<Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<UpdateServiceRequest, Result>
{
    private readonly IRepository<HelperService> _serviceRepo = serviceRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(UpdateServiceRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var service = await _serviceRepo.GetByIdAsync(request.Id, ct);
        if (service is null)
            return Result.Failure(HelperProfileErrors.ServiceNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null || service.HelperId != helper.Id)
            return Result.Failure(HelperProfileErrors.Unauthorized);

        service.Title = request.Title;
        service.Description = request.Description;
        service.Price = request.Price;
        service.DiscountPrice = request.DiscountPrice;
        service.DeliveryDays = request.DeliveryDays;
        service.RevisionsCount = request.RevisionsCount;
        service.Category = request.Category;
        service.UpdatedById = userId;
        service.UpdatedOn = DateTime.UtcNow;

        await _serviceRepo.UpdateAsync(service, ct);
        await _serviceRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
