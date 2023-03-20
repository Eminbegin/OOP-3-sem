namespace Banks.Console.Handlers.CreatingHandlers;

public abstract class CreatingHandler
{
    public CreatingHandler? Successor { get; set; }
    public abstract void HandleRequest(int condition);
}