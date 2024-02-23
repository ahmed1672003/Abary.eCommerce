namespace Pavon.Domain.Abstractions.Common;

public interface ISoftDeleteable
{
    bool IsDeleted { get; set; }

    Task UndoSoftDeleteAsync() =>
        Task.Run(() =>
        {
            IsDeleted = false;
        });

    Task DeleteAsync() =>
        Task.Run(() =>
        {
            IsDeleted = true;
        });
}
