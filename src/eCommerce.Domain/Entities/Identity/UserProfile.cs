﻿namespace eCommerce.Domain.Entities.Identity;

public sealed class UserProfile
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor
    public UserProfile() => Id = Guid.NewGuid();
    #endregion

    #region Keys
    [ForeignKey(nameof(Address))]
    public Guid? AddressId { get; set; }

    [ForeignKey(nameof(SocialMedia))]
    public Guid? SocialMediaId { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public Guid CreatedBy { get; set; } = Guid.Empty;

    public Guid DeletedBy { get; set; } = Guid.Empty;

    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [ForeignKey(nameof(AddressId))]
    public Address Address { get; set; }

    [ForeignKey(nameof(SocialMediaId))]
    public SocialMedia SocialMedia { get; set; }
    #endregion
}
