using Wasla.Common.AIIntegration;
using Wasla.Common.Gamification;

namespace Wasla.Features.AIIntegration.UseAIFeature;

public class UseAIFeatureHandler(
    IRepository<Helper> helperRepo,
    IRepository<AIUsage> aiUsageRepo,
    GamificationHelper gamificationHelper,
    IAIService aiService,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<UseAIFeatureRequest, Result<UseAIFeatureResponse>>
{
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<AIUsage> _aiUsageRepo = aiUsageRepo;
    private readonly GamificationHelper _gamificationHelper = gamificationHelper;
    private readonly IAIService _aiService = aiService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<UseAIFeatureResponse>> Handle(UseAIFeatureRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        // 1. Validate user has a helper profile
        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null)
            return Result.Failure<UseAIFeatureResponse>(AIIntegrationErrors.HelperNotFound);

        // 2. Check points cost
        var pointsCost = AIPointsCost.GetCost(request.FeatureType);
        var isFree = AIPointsCost.IsFree(request.FeatureType);

        // 3. Check helper has enough points
        if (!isFree && helper.Points < pointsCost)
            return Result.Failure<UseAIFeatureResponse>(AIIntegrationErrors.InsufficientPoints);

        // 4. Deduct points BEFORE AI call
        if (!isFree)
        {
            await _gamificationHelper.DeductPointsAsync(
                helper, pointsCost,
                $"AI Feature: {request.FeatureType}",
                null, "AIUsage", userId!, ct);

            await _helperRepo.UpdateAsync(helper, ct);
        }

        // 5. Call AI API (with failure handling)
        var aiResult = await _aiService.CallAIFeatureAsync(request.FeatureType, request.Input, ct);

        if (aiResult.IsFailure)
        {
            // REFUND points on failure
            if (!isFree)
            {
                await _gamificationHelper.AddPointsAsync(
                    helper, pointsCost, PointTransactionType.Bonus,
                    $"AI Feature refund (failed): {request.FeatureType}",
                    null, "AIUsage", userId!, ct);

                await _helperRepo.UpdateAsync(helper, ct);
            }

            // Store failed AIUsage record
            var failedUsage = new AIUsage
            {
                UserId = userId!,
                FeatureType = request.FeatureType,
                PointsCost = isFree ? 0 : pointsCost,
                InputLength = request.Input.Length,
                Status = AIRequestStatus.Failed,
                ErrorMessage = aiResult.Error.Description,
                UsedAt = DateTime.UtcNow,
                CreatedById = userId!,
                CreatedOn = DateTime.UtcNow
            };

            await _aiUsageRepo.AddAsync(failedUsage, ct);
            await _aiUsageRepo.SaveChangesAsync(ct);

            return Result.Failure<UseAIFeatureResponse>(
                new Error("AI.Failed", aiResult.Error.Description, StatusCodes.Status502BadGateway));
        }

        // 6. Store successful AIUsage
        var usage = new AIUsage
        {
            UserId = userId!,
            FeatureType = request.FeatureType,
            PointsCost = isFree ? 0 : pointsCost,
            InputLength = request.Input.Length,
            OutputLength = aiResult.Value.Length,
            Status = AIRequestStatus.Success,
            UsedAt = DateTime.UtcNow,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _aiUsageRepo.AddAsync(usage, ct);
        await _aiUsageRepo.SaveChangesAsync(ct);

        return Result.Success(new UseAIFeatureResponse(
            aiResult.Value,
            isFree ? 0 : pointsCost,
            AIRequestStatus.Success
        ));
    }
}
