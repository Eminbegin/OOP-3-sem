using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeBankHandlers;

public class DebitPercentageHandler : ChangeBankHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 2)
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

            decimal value = AnsiConsole.Ask<decimal>(BankChangesMessages.DebitPercentage);
            try
            {
                state = cb.SetDebitPercentage(bankId, value);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{BankChangesMessages.BadDebitPercentage(bankId)} {exception.Message}");
                return;
            }

            if (state)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.GoodDebitPercentage(bankId));
            }
            else
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.BadDebitPercentage(bankId));
            }
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}