using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeBankHandlers;

public class DepositDaysHandler : ChangeBankHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 4)
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

            int value = AnsiConsole.Ask<int>(BankChangesMessages.DepositDays);
            try
            {
                state = cb.SetDepositDays(bankId, value);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{BankChangesMessages.BadDepositDays(bankId)} {exception.Message}");
                return;
            }

            if (state)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.GoodDepositDays(bankId));
            }
            else
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.BadDepositDays(bankId));
            }
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}