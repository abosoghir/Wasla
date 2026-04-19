namespace Wasla.Features.Marketplace;

public static class MarketplaceErrors
{
    public static readonly Error TaskNotFound =
        new("Marketplace.TaskNotFound", "Task not found.", StatusCodes.Status404NotFound);

    public static readonly Error TaskNotOpen =
        new("Marketplace.TaskNotOpen", "Task is not in Open status.", StatusCodes.Status400BadRequest);

    public static readonly Error TaskNotInProgress =
        new("Marketplace.TaskNotInProgress", "Task is not in InProgress status.", StatusCodes.Status400BadRequest);

    public static readonly Error CannotOfferOwnTask =
        new("Marketplace.CannotOfferOwnTask", "You cannot create an offer on your own task.", StatusCodes.Status400BadRequest);

    public static readonly Error OfferNotFound =
        new("Marketplace.OfferNotFound", "Offer not found.", StatusCodes.Status404NotFound);

    public static readonly Error OfferNotPending =
        new("Marketplace.OfferNotPending", "Offer is not in Pending status.", StatusCodes.Status400BadRequest);

    public static readonly Error ProjectNotFound =
        new("Marketplace.ProjectNotFound", "Project not found.", StatusCodes.Status404NotFound);

    public static readonly Error MilestoneNotFound =
        new("Marketplace.MilestoneNotFound", "Milestone not found.", StatusCodes.Status404NotFound);

    public static readonly Error MilestoneNotSubmitted =
        new("Marketplace.MilestoneNotSubmitted", "Milestone is not in Submitted status.", StatusCodes.Status400BadRequest);

    public static readonly Error MilestoneNotPending =
        new("Marketplace.MilestoneNotPending", "Milestone must be Pending or InProgress to submit deliverables.", StatusCodes.Status400BadRequest);

    public static readonly Error Unauthorized =
        new("Marketplace.Unauthorized", "You are not authorized to perform this action.", StatusCodes.Status403Forbidden);

    public static readonly Error SeekerNotFound =
        new("Marketplace.SeekerNotFound", "Seeker profile not found.", StatusCodes.Status404NotFound);

    public static readonly Error HelperNotFound =
        new("Marketplace.HelperNotFound", "Helper profile not found.", StatusCodes.Status404NotFound);

    public static readonly Error AlreadyOffered =
        new("Marketplace.AlreadyOffered", "You have already submitted an offer for this task.", StatusCodes.Status409Conflict);
}
