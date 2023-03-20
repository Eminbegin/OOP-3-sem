namespace Backups.Exceptions;

public class RepositoryExceptions : Exception
{
    private RepositoryExceptions(string message)
        : base(message) { }
    public static RepositoryExceptions ItemNoExistsInRepository(string name)
        => new RepositoryExceptions($"{name} no exists on repository");
}