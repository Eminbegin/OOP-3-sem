using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ActionHandlers;

public class TransferActionHandler : ActionHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 2)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int id = 0;
            int accountIdFrom = AnsiConsole.Ask<int>(ActionMessages.Deposit);
            try
            {
                cb.IsAccountExists(accountIdFrom);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ActionMessages.UnSuccessful()} {exception.Message}");
                return;
            }

            int accountIdTo = AnsiConsole.Ask<int>(ActionMessages.Withdraw);
            try
            {
                cb.IsAccountExists(accountIdTo);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ActionMessages.UnSuccessful()} {exception.Message}");
                return;
            }

            decimal money = AnsiConsole.Ask<decimal>(ActionMessages.Balance);
            try
            {
                id = cb.MakeTransfer(accountIdFrom, accountIdTo, money);
            }
            catch (ActionExceptions exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ActionMessages.UnSuccessful()} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ActionMessages.Successful(id)}");
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}
