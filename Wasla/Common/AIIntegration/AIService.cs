using System.Text;
using System.Text.Json;
using Wasla.Common.Enums;
using Wasla.Common.ResultPattern;

namespace Wasla.Common.AIIntegration;

public class AIService(HttpClient httpClient, ILogger<AIService> logger) : IAIService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<AIService> _logger = logger;

    public async Task<Result<string>> CallAIFeatureAsync(AIFeatureType featureType, string input, CancellationToken ct)
    {
        try
        {
            var requestBody = new
            {
                feature = featureType.ToString(),
                input
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/ai/process", content, ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("AI service returned {StatusCode} for feature {Feature}",
                    response.StatusCode, featureType);

                return Result.Failure<string>(AIServiceErrors.ServiceUnavailable);
            }

            var result = await response.Content.ReadAsStringAsync(ct);
            return Result.Success(result);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to connect to AI service for feature {Feature}", featureType);
            return Result.Failure<string>(AIServiceErrors.ServiceUnavailable);
        }
        catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
        {
            _logger.LogError(ex, "AI service request timed out for feature {Feature}", featureType);
            return Result.Failure<string>(AIServiceErrors.ServiceUnavailable);
        }
    }
}

public static class AIServiceErrors
{
    public static readonly Error ServiceUnavailable =
        new("AI.ServiceUnavailable", "AI service is currently unavailable. Please try again later.", StatusCodes.Status503ServiceUnavailable);
}
