using EduBrain.Common.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EduBrain.Common.Caching;

public class CacheService(IDistributedCache distributedCache, ILogger<CacheService> logger) : ICacheService
{
    private readonly IDistributedCache _distributedCache = distributedCache;
    private readonly ILogger<CacheService> _logger = logger;

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        _logger.LogInformation("Get cache with key: {key}", key);

        var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        return cachedValue is null
            ? null
            : JsonSerializer.Deserialize<T>(cachedValue);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow =
           expiration ?? TimeSpan.FromHours(3)
        };

        _logger.LogInformation(
            "Set cache with key: {key}, Expiration: {expiration}",
            key,
            options.AbsoluteExpirationRelativeToNow
        );


        await _distributedCache.SetStringAsync(
            key,
            JsonSerializer.Serialize(value),
            options,
            cancellationToken
        );
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Remove cache with key: {key}", key);

        await _distributedCache.RemoveAsync(key, cancellationToken);
    }
}
