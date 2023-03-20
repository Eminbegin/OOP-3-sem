using Backups.Extra.Exceptions;

namespace Backups.Extra.Logging;

public class LoggerWithDate : ILogger
{
    private readonly ILogger _logger;

    public LoggerWithDate(ILogger logger)
    {
        if (logger is LoggerWithDate)
        {
            BackupExtraExceptions.DateLogger();
        }

        _logger = logger;
    }

    public void Log(string message)
    {
        _logger.Log($"{message} : {DateTime.Now.ToShortDateString()}");
    }
}