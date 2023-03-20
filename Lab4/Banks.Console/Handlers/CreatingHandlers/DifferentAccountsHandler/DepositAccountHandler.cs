using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Spectre.Console;

namespace Banks.Console.Handlers.CreatingHandlers.DifferentAccountsHandler;

public class DepositAccountHandler : AccountsHandler
{
    public override void HandleRequest(int condition, int clientId)
    {
        if (condition == 2)
        {
            decimal balance = AnsiConsole.Ask<decimal>(AccountCreationMessages.Balance);
            ICentralBank cb = CentralBank.GetInstance();
            cb.AddDepositAccount(clientId, balance);
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, AccountCreationMessages.DepositCreated(clientId, balance));
        }
        else
        {
            Successor?.HandleRequest(condition, clientId);
        }
    }
}