namespace Backups.Extra.Exceptions;

public class SelectorsExceptions : Exception
{
    private SelectorsExceptions(string message)
        : base(message) { }

    public static SelectorsExceptions LimitValue()
        => new SelectorsExceptions($"Limit is not positive");

    public static SelectorsExceptions BackupIsEmpty()
        => new SelectorsExceptions($"Backup is empty");
}