using Newtonsoft.Json;

namespace eCommerce.Presentation.Json.Service;

public sealed class JsonService : IJsonService
{
    public Task<string> SeralizeAsync(object value, CancellationToken ct = default)
    {
        if (value == null)
            return Task.FromResult(string.Empty);

        var jsonValue = JsonConvert.SerializeObject(
            value,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, }
        );
        return Task.FromResult(jsonValue);
    }

    public Task<T> DeseralizeAsync<T>(string jsonContent, CancellationToken ct = default)
    {
        var value = JsonConvert.DeserializeObject(jsonContent);

        return Task.FromResult((T)value);
    }
}
