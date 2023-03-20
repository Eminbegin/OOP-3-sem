using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Spectre.Console;

namespace Banks.Console.Handlers.CreatingHandlers.DifferentAccountsHandler;

public class DebitAccountHandler : AccountsHandler
{
    public override void HandleRequest(int condition, int clientId)
    {
        if (condition == 0)
        {
            decimal balance = AnsiConsole.Ask<decimal>(AccountCreationMessages.Balance);
            ICentralBank cb = CentralBank.GetInstance();
            cb.AddDebitAccount(clientId, balance);
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, AccountCreationMessages.DebitCreated(clientId, balance));
        }
        else
        {
            Successor?.HandleRequest(condition, clientId);
        }
    }
}