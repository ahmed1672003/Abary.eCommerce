namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

public sealed record CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public CreatUserProfile? Profile { get; set; }

    public sealed record CreatUserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public CreateAddressRequest? Address { get; set; }

        public sealed record CreateAddressRequest
        {
            public string? TowerNumber { get; set; }
            public string? TowerName { get; set; }
            public string? StreetNumber { get; set; }
            public string? StreetName { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
        }
    }
}
