using Backups.Entities;
using Backups.Exceptions;
using Backups.RepositoryItems;
using Zio;
using Zio.FileSystems;

namespace Backups.Repositories;

public class InMemoryRepository : IRepository, IDisposable
{
    private readonly Func<string, IReadOnlyCollection<IRepositoryItem>> _itemsFactory;
    private readonly Func<string, Stream> _streamCreator;
    private readonly MemoryFileSystem _fileSystem;
    private string _folderPath;

    public InMemoryRepository(MemoryFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        Separator = "//";
        _itemsFactory = GetRepositoryItems;
        _streamCreator = GetFileStream;
        _folderPath = string.Empty;
    }

    public string Separator { get; }

    public Stream GetStream(string fileName)
    {
        return _fileSystem.CreateFile(UPath.Combine(_folderPath, fileName));
    }

    public Stream GetStream(string folderPath, string archiveName)
    {
        return _fileSystem.OpenFile(UPath.Combine(folderPath, archiveName), FileMode.Open, FileAccess.Read);
    }

    public override string ToString()
    {
        return $"{_fileSystem.Name}";
    }

    public IRepositoryItem GetRepositoryItem(IBackupItem backupItem)
    {
        string name = backupItem.GetIdentifier();
        var uPath = new UPath(name);

        if (_fileSystem.FileExists(uPath))
        {
            string fullPath = _fileSystem.GetFileEntry(uPath).FullName;
            return new RepositoryFile(uPath.GetName(), fullPath, () => _streamCreator(fullPath));
        }

        if (_fileSystem.DirectoryExists(uPath))
        {
            string fullPath = _fileSystem.GetDirectoryEntry(uPath).FullName;
            return new RepositoryFolder(uPath.GetName(), fullPath, () => _itemsFactory(fullPath));
        }

        throw RepositoryExceptions.ItemNoExistsInRepository(backupItem.GetIdentifier());
    }

    public bool IsItemExists(string path)
    {
        var uPath = new UPath(path);
        return _fileSystem.FileExists(uPath) || _fileSystem.DirectoryExists(uPath);
    }

    public void DeleteItem(string path)
    {
        var uPath = new UPath(path);

        if (_fileSystem.FileExists(uPath))
        {
            _fileSystem.DeleteFile(uPath);
            return;
        }

        if (_fileSystem.DirectoryExists(uPath))
        {
            _fileSystem.DeleteDirectory(uPath, true);
        }

        throw new Exception();
    }

    public void CreateFolder(string name)
    {
        _folderPath = $"/{name}";
        _fileSystem.CreateDirectory(new UPath(_folderPath));
    }

    public void SaveLog(string path, string log)
    {
        _fileSystem.GetFileEntry(new UPath(path)).WriteAllText(log);
    }

    public void Dispose()
    {
        _fileSystem.Dispose();
    }

    private IReadOnlyCollection<IRepositoryItem> GetRepositoryItems(string path)
    {
        var uPath = new UPath(path);

        var repositoryFolders = new List<IRepositoryItem>(_fileSystem.EnumerateDirectories(uPath)
            .Select(d => new RepositoryFolder(d.GetName(), d.FullName, () => _itemsFactory(d.FullName))));

        var repositoryFiles = new List<IRepositoryItem>(_fileSystem.EnumerateFiles(uPath)
            .Select(f => new RepositoryFile(f.GetName(), f.FullName, () => _streamCreator(f.FullName))));

        return repositoryFiles.Concat(repositoryFolders).ToList();
    }

    private Stream GetFileStream(string path)
    {
        var uPath = new UPath(path);
        return _fileSystem.OpenFile(uPath, FileMode.Open, FileAccess.Read);
    }
}