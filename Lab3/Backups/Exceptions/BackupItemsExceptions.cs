namespace Backups.Exceptions;

public class BackupItemsExceptions : Exception
{
    private BackupItemsExceptions(string message)
        : base(message) { }

    public static BackupItemsExceptions AlreadyTracked(string name)
        => new BackupItemsExceptions($"Item {name} already tracked");

    public static BackupItemsExceptions ItemAlreadyNotTracked(string name)
        => new BackupItemsExceptions($"Item {name} already no tracked");
}