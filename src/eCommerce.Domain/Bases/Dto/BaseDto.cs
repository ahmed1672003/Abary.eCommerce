namespace eCommerce.Domain.Bases.Dto;

public record BaseDto<T>
{
    [JsonPropertyOrder(-1)]
    public T Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
}
