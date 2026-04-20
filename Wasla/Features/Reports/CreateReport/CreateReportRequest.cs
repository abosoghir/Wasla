namespace Wasla.Features.Reports.CreateReport;

public record CreateReportRequest(
    string? ReportedUserId,
    ReportType Type,
    int? RelatedEntityId,
    string? RelatedEntityType,
    string Reason,
    string? Details,
    string? EvidenceUrls,
    ReportSeverity Severity
) : IRequest<Result<CreateReportResponse>>;

public record CreateReportResponse(int Id);

public class CreateReportRequestValidator : AbstractValidator<CreateReportRequest>
{
    public CreateReportRequestValidator()
    {
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Details).MaximumLength(5000).When(x => x.Details != null);
        RuleFor(x => x.Severity).IsInEnum();
    }
}
