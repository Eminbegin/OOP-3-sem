namespace Banks.Console.Handlers.ActionHandlers;

public abstract class ActionHandler
{
    public ActionHandler? Successor { get; set; }
    public abstract void HandleRequest(int condition);
}