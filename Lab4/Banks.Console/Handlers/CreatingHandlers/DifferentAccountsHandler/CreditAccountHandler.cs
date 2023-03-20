using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Spectre.Console;

namespace Banks.Console.Handlers.CreatingHandlers.DifferentAccountsHandler;

public class CreditAccountHandler : AccountsHandler
{
    public override void HandleRequest(int condition, int clientId)
    {
        if (condition == 1)
        {
            decimal balance = AnsiConsole.Ask<decimal>(AccountCreationMessages.Balance);
            ICentralBank cb = CentralBank.GetInstance();
            cb.AddCreditAccount(clientId, balance);
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, AccountCreationMessages.CreditCreated(clientId, balance));
        }
        else
        {
            Successor?.HandleRequest(condition, clientId);
        }
    }
}