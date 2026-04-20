namespace Wasla.Features.Reports.CreateReport;

public class CreateReportHandler(
    IRepository<Report> reportRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateReportRequest, Result<CreateReportResponse>>
{
    private readonly IRepository<Report> _reportRepo = reportRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateReportResponse>> Handle(CreateReportRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure<CreateReportResponse>(ReportErrors.Unauthorized);

        if (request.ReportedUserId == userId)
            return Result.Failure<CreateReportResponse>(ReportErrors.CannotReportSelf);

        var report = new Report
        {
            ReporterId = userId,
            ReportedUserId = request.ReportedUserId,
            Type = request.Type,
            RelatedEntityId = request.RelatedEntityId,
            RelatedEntityType = request.RelatedEntityType,
            Reason = request.Reason,
            Details = request.Details,
            EvidenceUrls = request.EvidenceUrls,
            Severity = request.Severity
        };

        await _reportRepo.AddAsync(report, ct);
        await _reportRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateReportResponse(report.Id));
    }
}
