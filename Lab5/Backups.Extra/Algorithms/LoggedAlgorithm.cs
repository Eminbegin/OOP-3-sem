using Backups.Algorithms;
using Backups.Archivers;
using Backups.Extra.Logging;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Extra.Algorithms;

public class LoggedAlgorithm : IAlgorithm
{
    private IAlgorithm _algorithm;
    private ILogger _logger;

    public LoggedAlgorithm(IAlgorithm algorithm, ILogger logger)
    {
        _algorithm = algorithm;
        _logger = logger;
    }

    public IStorage Save(string folderName, string archiveName, IReadOnlyCollection<IRepositoryItem> items, IRepository repository, IArchiver archiver)
    {
        IStorage storage = _algorithm.Save(folderName, archiveName, items, repository, archiver);
        _logger.Log($"Storage added {storage}");
        return storage;
    }
}