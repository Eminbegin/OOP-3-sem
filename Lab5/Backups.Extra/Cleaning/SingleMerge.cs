using System.Drawing;
using Backups.Algorithms;
using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Extra.Cleaning;

public class SingleMerge : IMerge
{
    public SingleMerge(IRepository repository, IAlgorithm algorithm, IArchiver archiver)
    {
        Repository = repository;
        Algorithm = algorithm;
        Archiver = archiver;
    }

    public IRepository Repository { get; }
    public IAlgorithm Algorithm { get; }
    public IArchiver Archiver { get; }

    public void Resolve(IBackup backup, List<RestorePoint> points)
    {
        points = points.OrderByDescending(p => p.DateTime).ToList();
        var allBackupItems = new HashSet<IBackupItem>();
        var allRepositoryItems = new List<IRepositoryItem>();
        foreach (RestorePoint point in points)
        {
            foreach (IBackupItem item in point.Items)
            {
                if (allBackupItems.Add(item))
                {
                    allRepositoryItems.Add(GetRepositoryItem(point, item));
                }
            }
        }

        var id = Guid.NewGuid();
        DateTime date = DateTime.Now;
        IStorage storage = Algorithm.Save(GetFolderName(date), id.ToString(), allRepositoryItems, Repository, Archiver);
        var restorePoint = new RestorePoint(allBackupItems.ToList(), date, id, storage);
        backup.AddRestorePoint(restorePoint);
    }

    private IRepositoryItem GetRepositoryItem(RestorePoint restorePoint, IBackupItem item)
    {
        return restorePoint.Storage.GetItems().First(i => i.Name.Equals(item.Name));
    }

    private string GetFolderName(DateTime date) => $"{date:MM/dd/yyyy_HH-mm-ss}";
}