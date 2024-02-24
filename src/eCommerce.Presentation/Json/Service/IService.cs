namespace eCommerce.Presentation.Json.Service;

public interface IJsonService
{
    Task<string> SeralizeAsync(object value, CancellationToken ct = default);
    Task<T> DeseralizeAsync<T>(string jsonContent, CancellationToken ct = default);
}
