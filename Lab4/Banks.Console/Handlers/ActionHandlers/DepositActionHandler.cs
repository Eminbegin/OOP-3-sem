using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ActionHandlers;

public class DepositActionHandler : ActionHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 0)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int id = 0;
            int accountId = AnsiConsole.Ask<int>(ActionMessages.Deposit);
            try
            {
                cb.IsAccountExists(accountId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ActionMessages.UnSuccessful()} {exception.Message}");
                return;
            }

            decimal money = AnsiConsole.Ask<decimal>(ActionMessages.Balance);
            try
            {
                id = cb.AddMoney(accountId, money);
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