namespace Wasla.Common.Enums;

public enum VerificationType
{
    Identity = 1,
    Phone = 2,
    Email = 3,
    Address = 4,
    Professional = 5,
    Company = 6
}

public enum VerificationStatus
{
    Pending = 0,
    UnderReview = 1,
    Approved = 2,
    Rejected = 3,
    Expired = 4
}
