using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeBankHandlers;

public class LimitForDoubtfulHandler : ChangeBankHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 0)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int bankId = AnsiConsole.Ask<int>(BankChangesMessages.Bank);
            bool state = false;
            try
            {
                cb.GetBankById(bankId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, exception.Message);
                return;
            }

            decimal value = AnsiConsole.Ask<decimal>(BankChangesMessages.LimitForDoubtful);
            try
            {
                state = cb.SetLimitForDoubtful(bankId, value);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{BankChangesMessages.BadLimitForDoubtful(bankId)} {exception.Message}");
                return;
            }

            if (state)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.GoodLimitForDoubtful(bankId));
            }
            else
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.BadLimitForDoubtful(bankId));
            }
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}