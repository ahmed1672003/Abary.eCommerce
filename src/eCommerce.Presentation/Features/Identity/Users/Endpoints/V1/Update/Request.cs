namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Update;

public sealed record UpdateUserRequest
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UpdateUserProfile? Profile { get; set; }

    public record UpdateUserProfile
    {
        public Guid ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public UpdateAddressRequest? Address { get; set; }

        public record UpdateAddressRequest
        {
            public Guid AddressId { get; set; }
            public string? TowerNumber { get; set; }
            public string? TowerName { get; set; }
            public string? StreetNumber { get; set; }
            public string? StreetName { get; set; }
            public string? City { get; set; }
            public string Country { get; set; }
        }
    }
}
