namespace Banks.Console.Handlers.StartHandlers;

public class InfoAboutStartHandler : StartHandler
{
    public override void HandleRequest(int condition, string text)
    {
        if (condition == 5)
        {
        }
        else
        {
            Successor?.HandleRequest(condition, text);
        }
    }
}