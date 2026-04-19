namespace Wasla.Common.Enums;

public enum PaymentPurpose
{
    Task = 1,
    Project = 2,
    Session = 3,
    Service = 4,
    Subscription = 5
}

public enum PaymentMethod
{
    Wallet = 1,
    VodafoneCash = 2,
    InstaPay = 3,
    BankCard = 4,
    BankTransfer = 5
}

public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Refunded = 4,
    Disputed = 5
}
