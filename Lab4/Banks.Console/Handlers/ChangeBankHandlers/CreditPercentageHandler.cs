using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeBankHandlers;

public class CreditPercentageHandler : ChangeBankHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 3)
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

            decimal value = AnsiConsole.Ask<decimal>(BankChangesMessages.CreditPercentage);
            try
            {
                state = cb.SetCreditPercentage(bankId, value);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{BankChangesMessages.BadCreditPercentage(bankId)} {exception.Message}");
                return;
            }

            if (state)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.GoodCreditPercentage(bankId));
            }
            else
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.BadCreditPercentage(bankId));
            }
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}