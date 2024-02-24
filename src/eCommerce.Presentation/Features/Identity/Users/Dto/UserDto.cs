namespace eCommerce.Presentation.Features.Identity.Users.Dto;

public sealed record UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedOn { get; set; }
    public UserProfileDto Profile { get; set; }

    public sealed record UserProfileDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public AddressDto Address { get; set; }

        public sealed record AddressDto
        {
            public string? TowerNumber { get; set; }
            public string? TowerName { get; set; }
            public string? StreetNumber { get; set; }
            public string? StreetName { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public DateTime CreatedOn { get; set; }
        }
    }
}
