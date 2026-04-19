namespace Wasla.Common.Enums;

public enum NotificationType
{
    NewOffer = 1,
    OfferAccepted = 2,
    OfferRejected = 3,
    SessionConfirmed = 4,
    SessionReminder = 5,
    SessionCompleted = 6,
    TaskCompleted = 7,
    PaymentReceived = 8,
    PaymentSuccess = 9,
    MessageReceived = 10,
    ReviewReceived = 11,
    MilestoneApproved = 12,
    MilestoneSubmitted = 13,
    WithdrawalCompleted = 14,
    SystemAnnouncement = 15,
    ProfileVerified = 16
}

public enum NotificationPriority
{
    Low = 1,
    Normal = 2,
    High = 3,
    Urgent = 4
}

public enum NotificationChannel
{
    InApp = 1,
    Email = 2,
    SMS = 3,
    Push = 4
}
