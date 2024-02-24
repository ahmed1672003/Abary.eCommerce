namespace eCommerce.Presentation.Jwt.Dto;

public sealed record TokenDto
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}
