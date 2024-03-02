namespace eCommerce.Domain.Abstractions.Common;

public interface ITrackableCreate<TCreator>
    where TCreator : struct
{
    DateTime CreatedOn { get; set; }

    TCreator CreatedBy { get; set; }

    Task CreateAsync() =>
        Task.Run(() =>
        {
            CreatedOn = DateTime.UtcNow;
        });
}
