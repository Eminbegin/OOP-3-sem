using Backups.Algorithms;
using Backups.Archivers;
using Backups.Entities;
using Backups.Extra.Cleaning;
using Backups.Extra.Logging;
using Backups.Extra.Selectors;
using Backups.Repositories;

namespace Backups.Extra.Entities;

public interface INameBuilder
{
    IAlgorithmBuilder WithName(string name);
}

public interface IAlgorithmBuilder
{
    IArchiverBuilder WithAlgorithm(IAlgorithm algorithm);
}

public interface IArchiverBuilder
{
    IRepositoryBuilder WithArchiver(IArchiver archiver);
}

public interface IRepositoryBuilder
{
    ISelectorBuilder WithRepository(IRepository repository);
}

public interface ISelectorBuilder
{
    ILoggerBuilder WithSelector(ISelector selector);
}

public interface ILoggerBuilder
{
    IResolverBuilder WithLogger(ILogger logger);
}

public interface IResolverBuilder
{
    IBackupBuilder WithResolver(IResolver resolver);
}

public interface IBackupBuilder
{
    IOtherBuilder WithBackup(IBackup backup);
}

public interface IOtherBuilder
{
    ExtraBackupTask Build();
}