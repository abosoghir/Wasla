namespace Wasla.Features.HelperProfileManagement.HelperServices.DeleteService;

public class DeleteServiceHandler(
    IRepository<HelperService> serviceRepo,
    IRepository<Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<DeleteServiceRequest, Result>
{
    private readonly IRepository<HelperService> _serviceRepo = serviceRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(DeleteServiceRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var service = await _serviceRepo.GetByIdAsync(request.Id, ct);
        if (service is null)
            return Result.Failure(HelperProfileErrors.ServiceNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null || service.HelperId != helper.Id)
            return Result.Failure(HelperProfileErrors.Unauthorized);

        service.IsActive = false;
        service.IsDeleted = true;
        service.UpdatedById = userId;
        service.UpdatedOn = DateTime.UtcNow;

        await _serviceRepo.UpdateAsync(service, ct);
        await _serviceRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
