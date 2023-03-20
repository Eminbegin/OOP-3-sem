namespace Backups.Extra.Exceptions;

public class BackupExtraExceptions : Exception
{
    private BackupExtraExceptions(string message)
        : base(message) { }

    public static BackupExtraExceptions NullFields()
        => new BackupExtraExceptions($"Null fields");

    public static BackupExtraExceptions DateLogger()
        => new BackupExtraExceptions($"Logger already with date");

    public static BackupExtraExceptions NullItem()
        => new BackupExtraExceptions($"Item not founded");
}