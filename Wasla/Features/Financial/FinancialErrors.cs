namespace Wasla.Features.Financial;

public static class FinancialErrors
{
    public static readonly Error WalletNotFound =
        new("Financial.WalletNotFound", "Wallet not found.", StatusCodes.Status404NotFound);

    public static readonly Error InsufficientBalance =
        new("Financial.InsufficientBalance", "Insufficient wallet balance.", StatusCodes.Status400BadRequest);

    public static readonly Error PaymentNotFound =
        new("Financial.PaymentNotFound", "Payment not found.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidPaymentStatus =
        new("Financial.InvalidPaymentStatus", "Payment is not in the correct status.", StatusCodes.Status400BadRequest);

    public static readonly Error Unauthorized =
        new("Financial.Unauthorized", "You are not authorized to perform this action.", StatusCodes.Status403Forbidden);
}
