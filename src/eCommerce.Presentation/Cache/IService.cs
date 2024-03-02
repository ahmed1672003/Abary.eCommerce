namespace eCommerce.Presentation.Cache;

public interface ICacheService
{
    Task<TValue> SetAsync<TValue>(
        object key,
        TValue value,
        DateTimeOffset? expiration = null,
        CancellationToken ct = default
    );
    Task<TValue> GetAsync<TValue>(object key, bool restore = false, CancellationToken ct = default);
    Task RemoveAsync(object key, CancellationToken ct = default);
}
