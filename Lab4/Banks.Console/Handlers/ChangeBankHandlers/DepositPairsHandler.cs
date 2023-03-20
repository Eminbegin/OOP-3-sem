using Banks.BanksSystem;
using Banks.BanksSystem.BankConfigurations;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeBankHandlers;

public class DepositPairsHandler : ChangeBankHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 5)
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

            DepositPercentages? dp = CreatePairs(bankId);
            if (dp is null)
            {
                return;
            }

            state = cb.SetDepositPercentages(bankId, dp);
            if (state)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.GoodDepositPairs(bankId));
            }
            else
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankChangesMessages.BadDepositPairs(bankId));
            }
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }

    private DepositPercentages? CreatePairs(int bankId)
    {
        IPairsBuilder builder = DepositPercentages.Builder;
        int count = AnsiConsole.Ask<int>(BankCreatingMessages.DepositPairs);
        if (count < 0)
        {
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"[red]{BankChangesMessages.BadDepositPairs(bankId)}[/] Количество не может быть отрицательным");
            return null;
        }

        for (int i = 0; i < count; i++)
        {
            decimal value = AnsiConsole.Ask<decimal>(BankCreatingMessages.Value);
            decimal percentage = AnsiConsole.Ask<decimal>(BankCreatingMessages.Percentage);
            try
            {
                builder.AddPair(value, percentage);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"[red]{BankChangesMessages.BadDepositPairs(bankId)}[/] {exception.Message}");
                return null;
            }
        }

        decimal newPercentage = AnsiConsole.Ask<decimal>(BankCreatingMessages.Percentage);
        try
        {
            return builder.Build(newPercentage);
        }
        catch (ExistenceException exception)
        {
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"[red]{BankChangesMessages.BadDepositPairs(bankId)}[/] {exception.Message}");
            return null;
        }
    }
}