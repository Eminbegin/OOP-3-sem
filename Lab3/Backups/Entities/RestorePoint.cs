using Backups.Storages;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<IBackupItem> _restoredItems;
    public RestorePoint(List<IBackupItem> backupItems, DateTime dateTime, Guid id, IStorage storage)
    {
        Id = id;
        Storage = storage;
        _restoredItems = backupItems;
        DateTime = dateTime;
    }

    public Guid Id { get; }
    public DateTime DateTime { get; set; }
    public IStorage Storage { get; }
    public List<IBackupItem> Items => _restoredItems;

    public override string ToString()
    {
        return $"(Id: {Id}, Date: {DateTime.ToShortDateString()}, Storage: {Storage})";
    }
}