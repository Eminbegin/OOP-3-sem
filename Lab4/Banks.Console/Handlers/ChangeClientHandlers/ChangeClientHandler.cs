namespace Banks.Console.Handlers.ChangeClientHandlers;

public abstract class ChangeClientHandler
{
    public ChangeClientHandler? Successor { get; set; }
    public abstract void HandleRequest(int condition);
}