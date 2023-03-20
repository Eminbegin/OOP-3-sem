using Backups.Algorithms;
using Backups.Archivers;
using Backups.Repositories;

namespace Backups.Entities;

public interface IBackupTask
{
    IAlgorithm Algorithm { get; }
    IArchiver Archiver { get; }
    IBackup Backup { get; }
    IRepository Repository { get; }
    string Name { get; }
    void TrackObject(IBackupItem backupItem);
    void UntrackObject(IBackupItem backupItem);
    void CreateRestorePoint();
}