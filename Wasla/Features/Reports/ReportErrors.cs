namespace Wasla.Features.Reports;

public static class ReportErrors
{
    public static readonly Error Unauthorized =
        new("Report.Unauthorized", "You are not authorized to perform this action.", StatusCodes.Status403Forbidden);

    public static readonly Error CannotReportSelf =
        new("Report.CannotReportSelf", "You cannot report yourself.", StatusCodes.Status400BadRequest);
}
