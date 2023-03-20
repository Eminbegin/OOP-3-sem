namespace Banks.Console.Handlers.ChangeBankHandlers;

public abstract class ChangeBankHandler
{
    public ChangeBankHandler? Successor { get; set; }
    public abstract void HandleRequest(int condition);
}