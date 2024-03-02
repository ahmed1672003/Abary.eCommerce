namespace eCommerce.Presentation.Jwt.Dto;

public sealed record TokenDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Schema { get; set; }
    public string LoginProvider { get; set; }
    public long ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public string Value { get; set; }
}
