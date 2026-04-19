namespace Wasla.Common.Enums;

public enum ReportType
{
    User = 1,
    Task = 2,
    Project = 3,
    Message = 4,
    Post = 5,
    Comment = 6,
    Review = 7,
    Payment = 8,
    Other = 9
}

public enum ReportStatus
{
    Pending = 0,
    UnderReview = 1,
    Resolved = 2,
    Dismissed = 3,
    Escalated = 4
}

public enum ReportSeverity
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}
