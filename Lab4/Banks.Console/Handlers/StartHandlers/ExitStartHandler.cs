namespace Banks.Console.Handlers.StartHandlers;

public class ExitStartHandler : StartHandler
{
    public override void HandleRequest(int condition, string text)
    {
        if (condition == 6)
        {
        }
        else
        {
            Successor?.HandleRequest(condition, text);
        }
    }
}