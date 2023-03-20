using Backups.Algorithms;
using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void CreateRestorePoint()
    {
        var fs = new MemoryFileSystem();
        fs.WriteAllText("/temp.txt", "AbobaAboba");
        fs.CreateDirectory("/testDir");
        fs.WriteAllText("/testDir/aaa.txt", "ZalupaSlonika");
        fs.EnumerateDirectoryEntries("/testDir");
        fs.WriteAllText("/testDir/aa2a.txt", "ZalupaSlonika");
        IAlgorithm algorithm = new SingleAlgorithm();
        IBackup backup = new SimpleBackup();
        IArchiver archiver = new ZipArchiver(".zip");
        IRepository repository = new InMemoryRepository(fs);
        IBackupItem file1 = new BackupItem("/temp.txt", repository);
        IBackupItem directory1 = new BackupItem("/testDir", repository);
        IBackupItem file2 = new BackupItem("/testDir/aa2a.txt", repository);
        var backupTask = new BackupTask("Aboba", algorithm, archiver, backup, repository);
        backupTask.TrackObject(file1);
        backupTask.TrackObject(file2);
        backupTask.TrackObject(directory1);
        backupTask.CreateRestorePoint();
        Assert.True(true);
    }
}