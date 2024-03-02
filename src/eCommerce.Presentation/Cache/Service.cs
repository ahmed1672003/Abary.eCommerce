using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace eCommerce.Presentation.Cache;

public sealed class CacheService : ICacheService
{
    readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<TValue> SetAsync<TValue>(
        object key,
        TValue value,
        DateTimeOffset? expiration = null,
        CancellationToken ct = default
    )
    {
        if (key == null || value is null)
            return Task.FromResult(default(TValue));

        TValue cacheValue = default;
        if (expiration.HasValue)
        {
            cacheValue = _memoryCache.Set(key, value, expiration.Value);

            return Task.FromResult(cacheValue);
        }
        cacheValue = _memoryCache.Set(key, value);

        return Task.FromResult(cacheValue);
    }

    public Task<TValue> GetAsync<TValue>(
        object key,
        bool restore = false,
        CancellationToken ct = default
    )
    {
        if (key == null)
            return Task.FromResult(default(TValue));

        TValue value = default;

        value = _memoryCache.Get<TValue>(key);

        if (value == null && restore)
        {
            value = _memoryCache.Set(key, value);
        }

        return Task.FromResult(value);
    }

    public Task RemoveAsync(object key, CancellationToken ct = default)
    {
        if (key == null)
            return Task.CompletedTask;

        _memoryCache.Remove(key);

        return Task.CompletedTask;
    }
}
