using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ActionHandlers;

public class CanceledActionHandler : ActionHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 3)
        {
            int id = AnsiConsole.Ask<int>(ActionMessages.Cancel);
            ICentralBank cb = CentralBank.GetInstance();
            try
            {
                cb.RemoveTransaction(id);
            }
            catch (ActionExceptions exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ActionMessages.NotCanceled()} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, ActionMessages.Canceled(id));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}