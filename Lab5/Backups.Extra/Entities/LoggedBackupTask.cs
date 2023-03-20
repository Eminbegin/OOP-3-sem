using Backups.Algorithms;
using Backups.Archivers;
using Backups.Entities;
using Backups.Extra.Cleaning;
using Backups.Extra.Exceptions;
using Backups.Extra.Logging;
using Backups.Extra.Selectors;
using Backups.Repositories;

namespace Backups.Extra.Entities;

public class ExtraBackupTask : IBackupTask
{
    private readonly IBackupTask _backupTask;
    private readonly ILogger _logger;

    private ExtraBackupTask(
        IAlgorithm algorithm,
        IArchiver archiver,
        IBackup backup,
        ILogger logger,
        IRepository repository,
        string name)
    {
        Algorithm = algorithm;
        Archiver = archiver;
        Backup = backup;
        Name = name;
        Repository = repository;
        _backupTask = new BackupTask(name, algorithm, archiver, backup, repository);
        _logger = logger;
    }

    public IAlgorithm Algorithm { get; }
    public IArchiver Archiver { get; }
    public IBackup Backup { get; }
    public IRepository Repository { get; }
    public string Name { get; }

    public INameBuilder Builder => new BackupTaskBuilder();

    public void TrackObject(IBackupItem backupItem)
    {
        _backupTask.TrackObject(backupItem);
        _logger.Log($"Added item {backupItem} to backup task {Name}");
    }

    public void UntrackObject(IBackupItem backupItem)
    {
        _backupTask.UntrackObject(backupItem);
        _logger.Log($"Removed item {backupItem} from backup task {Name}");
    }

    public void CreateRestorePoint()
    {
        _backupTask.CreateRestorePoint();
        _logger.Log($"Restore point created");
    }

    private class BackupTaskBuilder : INameBuilder, IAlgorithmBuilder, IArchiverBuilder, IRepositoryBuilder, ISelectorBuilder, ILoggerBuilder, IResolverBuilder, IBackupBuilder, IOtherBuilder
    {
        private string _name = string.Empty;
        private IResolver? _resolver;
        private ILogger? _logger;
        private IAlgorithm? _algorithm;
        private IArchiver? _archiver;
        private IRepository? _repository;
        private ISelector? _selector;
        private IBackup? _backup;

        public IAlgorithmBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public IArchiverBuilder WithAlgorithm(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
            return this;
        }

        public IRepositoryBuilder WithArchiver(IArchiver archiver)
        {
            _archiver = archiver;
            return this;
        }

        public ISelectorBuilder WithRepository(IRepository repository)
        {
            _repository = repository;
            return this;
        }

        public ILoggerBuilder WithSelector(ISelector selector)
        {
            _selector = selector;
            return this;
        }

        public IResolverBuilder WithLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public IBackupBuilder WithResolver(IResolver resolver)
        {
            _resolver = resolver;
            return this;
        }

        public IOtherBuilder WithBackup(IBackup backup)
        {
            _backup = backup;
            return this;
        }

        public ExtraBackupTask Build()
        {
            if (_algorithm is null || _archiver is null || _backup is null || _logger is null || _repository is null || _selector is null || _resolver is null)
            {
                throw BackupExtraExceptions.NullFields();
            }

            IBackup extraBackup = new ExtraBackup(_backup, _resolver, _selector);
            return new ExtraBackupTask(_algorithm, _archiver, extraBackup, _logger, _repository, _name);
        }
    }
}