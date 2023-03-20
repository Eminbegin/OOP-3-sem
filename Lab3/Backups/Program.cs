using System.IO.Compression;
using Backups.Algorithms;
using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;
using Zio;
using Zio.FileSystems;

namespace Backups;

public static class Program
{
    private static void Main()
    {
        Test2();
    }

    private static void Test2()
    {
        var fs = new MemoryFileSystem();
        fs.WriteAllText("/temp.txt", "AbobaAboba");
        fs.CreateDirectory("/testDir");
        fs.WriteAllText("/testDir/aaa.txt", "ZalupaSlonika");
        fs.EnumerateDirectoryEntries("/testDir");
        fs.WriteAllText("/testDir/aa2a.txt", "ZalupaSlonika");
        IRepository rep = new FileSystemRepository(@"C:\Users\emink\Desktop\ree\");
        IAlgorithm alg = new SingleAlgorithm();
        IBackup backup = new SimpleBackup();
        IArchiver arc = new ZipArchiver(".zip");
        IRepository urep = new InMemoryRepository(fs);
        var urep2 = new FileSystemRepository(@"C:\Users\emink\Desktop\");
        var bt = new BackupTask("asd", alg, arc, backup, rep);
        IBackupItem aaa = new BackupItem("/temp.txt", urep);
        IBackupItem hdsgoi = new BackupItem("/testDir", urep);
        IBackupItem bbb = new BackupItem("/testDir/aa2a.txt", urep);
        IBackupItem asd = new BackupItem("1245.docx", urep2);
        bt.TrackObject(asd);
        bt.TrackObject(aaa);
        bt.TrackObject(hdsgoi);
        bt.TrackObject(bbb);
        bt.CreateRestorePoint();
    }

    private static void Test1()
    {
        IRepository rep = new FileSystemRepository(@"C:\Users\emink\Desktop\ree\");
        IAlgorithm alg = new SplitAlgorithm();
        IBackup backup = new SimpleBackup();
        IArchiver arc = new ZipArchiver(".zip");
        var urep = new FileSystemRepository(@"C:\Users\emink\Desktop\");
        var bt = new BackupTask("aboba", alg, arc, backup, rep);
        IBackupItem aaa = new BackupItem("Slavcha\\", urep);
        IBackupItem hdsgoi = new BackupItem(".jpg", urep);
        IBackupItem bbb = new BackupItem("Kysect_hws\\", urep);
        IBackupItem asd = new BackupItem("1245.docx", urep);
        bt.TrackObject(asd);
        bt.TrackObject(aaa);
        bt.TrackObject(hdsgoi);
        bt.TrackObject(bbb);
        bt.CreateRestorePoint();
        var bsdab = bt.Backup as SimpleBackup;
        IEnumerable<IRepositoryItem>? sdfsdgf = bsdab?.GetLastStorage().GetItems();
        bt.CreateRestorePoint();
    }
}