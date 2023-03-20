using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Entities;

public class BackupItem : IBackupItem
{
    private readonly string _identifier;

    public BackupItem(string identifier, IRepository repository)
    {
        _identifier = identifier;
        Repository = repository;
        int temp = Math.Max(identifier.LastIndexOf('\\'), identifier.LastIndexOf('/'));
        Name = identifier.Substring(temp, identifier.Length - temp);
    }

    public string Name { get; }
    public IRepository Repository { get; }
    public IRepositoryItem GetRepositoryItem() => Repository.GetRepositoryItem(this);

    public string GetIdentifier() => _identifier;

    public override string ToString()
    {
        return $"path in repo: {_identifier}, repo: {Repository}";
    }
}