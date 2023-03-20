using Backups.Algorithms;
using Backups.Archivers;
using Backups.Exceptions;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly HashSet<IBackupItem> _followingObjects;

    public BackupTask(
        string name,
        IAlgorithm algorithm,
        IArchiver archiver,
        IBackup backup,
        IRepository repository)
    {
        Algorithm = algorithm;
        Name = name;
        Archiver = archiver;
        Repository = repository;
        _followingObjects = new HashSet<IBackupItem>();
        Backup = backup;
    }

    public IAlgorithm Algorithm { get; }
    public IArchiver Archiver { get; }
    public IBackup Backup { get; }
    public IRepository Repository { get; }
    public string Name { get; }

    public void TrackObject(IBackupItem backupItem)
    {
        if (!_followingObjects.Add(backupItem))
        {
            throw BackupItemsExceptions.AlreadyTracked(backupItem.GetIdentifier());
        }
    }

    public void UntrackObject(IBackupItem backupItem)
    {
        if (_followingObjects.Remove(backupItem))
        {
            throw BackupItemsExceptions.ItemAlreadyNotTracked(backupItem.GetIdentifier());
        }
    }

    public void CreateRestorePoint()
    {
        DateTime nowDate = DateTime.Now;
        string folderName = GetFolderName(nowDate);
        var id = Guid.NewGuid();
        IReadOnlyCollection<IRepositoryItem> items = _followingObjects
            .Select(i => i.GetRepositoryItem())
            .ToList();
        IStorage storage = Algorithm.Save(folderName, id.ToString(), items, Repository, Archiver);
        var restorePoint = new RestorePoint(_followingObjects.ToList(), DateTime.Now, id, storage);
        Backup.AddRestorePoint(restorePoint);
    }

    private string GetFolderName(DateTime date) => $"{date:MM/dd/yyyy_HH-mm-ss}";
}