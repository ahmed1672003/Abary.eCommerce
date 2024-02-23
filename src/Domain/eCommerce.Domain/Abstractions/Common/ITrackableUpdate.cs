namespace Pavon.Domain.Abstractions.Common;

public interface ITrackableUpdate<TUpdater>
{
    DateTime UpdatedOn { get; set; }
    TUpdater? UpdatedBy { get; set; }

    Task UpdateAsync() =>
        Task.Run(() =>
        {
            UpdatedOn = DateTime.UtcNow;
        });
}
