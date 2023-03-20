namespace Banks.Console.Handlers.CreatingHandlers.DifferentAccountsHandler;

public abstract class AccountsHandler
{
    public AccountsHandler? Successor { get; set; }
    public abstract void HandleRequest(int condition, int clientId);
}