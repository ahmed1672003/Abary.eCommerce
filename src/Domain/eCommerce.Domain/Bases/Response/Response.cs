namespace eCommerce.Domain.Bases.Response;

public record Response
{
    public Response() => StatusCode = (int)HttpStatusCode.OK;

    [JsonPropertyOrder(1)]
    public bool IsSuccess { get; set; }

    [JsonPropertyOrder(2)]
    public int StatusCode { get; set; }

    [JsonPropertyOrder(3)]
    public string Message { get; set; }
}

public record Response<TResponse> : Response
{
    [JsonPropertyOrder(10)]
    public TResponse Result { get; set; }
}

public sealed record PaginationResponse<TResponse> : Response<TResponse>
{
    [JsonPropertyOrder(4)]
    public int PageSize { get; set; }

    [JsonPropertyOrder(5)]
    public int PageNumber { get; set; }

    [JsonPropertyOrder(6)]
    public int TotalCount { get; set; }

    [JsonPropertyOrder(7)]
    public int Count { get; set; }

    [JsonPropertyOrder(8)]
    public int TotalPages =>
        Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDecimal(PageSize)));

    [JsonPropertyOrder(9)]
    public bool MoveNext => PageNumber < TotalPages;

    [JsonPropertyOrder(10)]
    public bool MovePrevious => PageNumber > 1;
}
