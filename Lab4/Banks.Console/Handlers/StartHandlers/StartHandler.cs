namespace Banks.Console.Handlers.StartHandlers;

public abstract class StartHandler
{
    public StartHandler? Successor { get; set; }
    public abstract void HandleRequest(int condition, string text);
}