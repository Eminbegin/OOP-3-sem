using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.StartHandlers;

public class TimeStartHandler : StartHandler
{
    public override void HandleRequest(int condition, string text)
    {
        if (condition == 4)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int value = AnsiConsole.Ask<int>(TimeMessages.Days);
            try
            {
                cb.SkipNDays(value);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{TimeMessages.BadDays} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, TimeMessages.GoodDays(value));
        }
        else
        {
            Successor?.HandleRequest(condition, text);
        }
    }
}