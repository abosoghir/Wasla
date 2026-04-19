namespace Wasla.Common.Enums;

public enum TransactionType
{
    Deposit = 1,
    Withdrawal = 2,
    Payment = 3,
    Refund = 4,
    Fee = 5,
    Bonus = 6,
    ProjectPayment = 7,
    SessionPayment = 8
}

public enum TransactionStatus
{
    Pending = 0,
    Completed = 1,
    Failed = 2,
    Cancelled = 3
}
