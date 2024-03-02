namespace eCommerce.Presentation.Features.Identity.Users.Dto;

public sealed record UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedOn { get; set; }
    public UserProfileDto Profile { get; set; }
    public List<UserPermissionDto> UserPremissions { get; set; } = new(0);

    public class UserPermissionDto
    {
        public PermissionDto Permission { get; set; }

        public class PermissionDto
        {
            public Guid Id { get; set; }
            public EntityName Entity { get; set; }
            public ModuleName Module { get; set; }
            public string Value { get; set; }
            public DateTime CreatedOn { get; set; }
        }
    }

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
