namespace Pavon.Domain.Abstractions.Common;

public interface ITrackableDelete<TDeleter>
{
    DateTime? DeletedOn { get; set; }

    TDeleter DeletedBy { get; set; }

    Task DeleteAsync() =>
        Task.Run(() =>
        {
            DeletedOn = DateTime.UtcNow;
        });
}
