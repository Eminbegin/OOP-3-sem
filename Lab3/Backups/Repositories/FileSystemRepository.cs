using Backups.Entities;
using Backups.Exceptions;
using Backups.RepositoryItems;

namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    private readonly Func<string, IReadOnlyCollection<IRepositoryItem>> _itemsFactory;
    private readonly Func<string, Stream> _streamCreator;

    private readonly string _mainPath;
    private string _folderPath;

    public FileSystemRepository(string mainPath)
    {
        if (mainPath[^1] != Path.DirectorySeparatorChar)
        {
            throw new Exception();
        }

        _folderPath = string.Empty;
        _itemsFactory = GetRepositoryItems;
        _mainPath = mainPath;
        Separator = Path.DirectorySeparatorChar.ToString();
        _streamCreator = GetFileStream;
    }

    public string Separator { get; }

    public Stream GetStream(string fileName)
    {
        return new FileStream($"{_folderPath}{fileName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    public Stream GetStream(string folderPath, string archiveName)
    {
        char sep = Path.DirectorySeparatorChar;
        return File.Open($"{_mainPath}{folderPath}{sep}{archiveName}", FileMode.Open, FileAccess.Read);
    }

    public override string ToString()
    {
        return $"{_mainPath}";
    }

    public IRepositoryItem GetRepositoryItem(IBackupItem backupItem)
    {
        string fullPath = $"{_mainPath}{backupItem.GetIdentifier()}";

        if (File.Exists(fullPath))
        {
            string name = new FileInfo(fullPath).Name;
            return new RepositoryFile(name, fullPath, () => _streamCreator(fullPath));
        }

        if (Directory.Exists(fullPath))
        {
            string name = new DirectoryInfo(fullPath).Name;
            return new RepositoryFolder(name, fullPath, () => _itemsFactory(fullPath));
        }

        throw RepositoryExceptions.ItemNoExistsInRepository(backupItem.GetIdentifier());
    }

    public bool IsItemExists(string path)
    {
        string fullPath = $"{_mainPath}{path}";
        return File.Exists(fullPath) || Directory.Exists(fullPath);
    }

    public void DeleteItem(string path)
    {
        string fullPath = $"{_mainPath}{path}";

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        if (Directory.Exists(fullPath))
        {
            Directory.Delete(fullPath, true);
        }
    }

    public void CreateFolder(string name)
    {
        char sep = Path.DirectorySeparatorChar;
        _folderPath = $"{_mainPath}{name}{sep}";
        Directory.CreateDirectory(_folderPath);
    }

    public void SaveLog(string path, string log)
    {
        string fullPath = $"{_mainPath}{path}";
        File.WriteAllText(fullPath, log);
    }

    private IReadOnlyCollection<IRepositoryItem> GetRepositoryItems(string fullPath)
    {
        var directoryInfo = new DirectoryInfo(fullPath);
        var repositoryFolders = new List<IRepositoryItem>(directoryInfo
            .GetDirectories()
            .Select(d => new RepositoryFolder(d.Name, d.FullName, () => _itemsFactory(d.FullName))));

        var repositoryFiles = new List<IRepositoryItem>(directoryInfo
            .GetFiles()
            .Select(f => new RepositoryFile(f.Name, f.FullName, () => _streamCreator(f.FullName))));

        return repositoryFiles.Concat(repositoryFolders).ToList();
    }

    private Stream GetFileStream(string fullPath)
    {
        return File.Open(fullPath, FileMode.Open);
    }
}