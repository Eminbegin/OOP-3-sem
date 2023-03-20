using Backups.Repositories;

namespace Backups.Extra.Logging;

public class FileLogger : ILogger
{
    private readonly string _path;
    private readonly IRepository _repository;

    public FileLogger(string path, IRepository repository)
    {
        _path = path;
        _repository = repository;
    }

    public void Log(string message)
    {
        _repository.SaveLog(_path, message);
    }
}