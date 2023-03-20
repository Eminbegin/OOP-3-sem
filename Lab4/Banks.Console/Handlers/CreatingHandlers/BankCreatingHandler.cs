using Banks.BanksSystem;
using Banks.BanksSystem.BankConfigurations;
using Banks.BanksSystem.BankConfigurations.BankConfigurationBuilders;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.CreatingHandlers;

public class BankCreatingHandler : CreatingHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 1)
        {
            AnsiConsole.Clear();
            string name = AnsiConsole.Ask<string>(BankCreatingMessages.Name);
            ICentralBank cb = CentralBank.GetInstance();
            BankConfiguration? bc = CreateConfiguration();
            if (bc is null)
            {
                return;
            }

            int id = cb.AddBank(name, bc);
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, BankCreatingMessages.GetMessage(name, id));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }

    private BankConfiguration? CreateConfiguration()
    {
        DepositPercentages? dp = CreatePairs();
        if (dp is null)
        {
            return null;
        }

        try
        {
            return BankConfiguration.Builder
                .WithLimitForDoubtful(AnsiConsole.Ask<decimal>(BankCreatingMessages.LimitForDoubtful))
                .WithDebitPercentage(AnsiConsole.Ask<decimal>(BankCreatingMessages.DebitPercentage))
                .WithCreditPercentage(AnsiConsole.Ask<decimal>(BankCreatingMessages.CreditPercentage))
                .WithCreditLimit(AnsiConsole.Ask<decimal>(BankCreatingMessages.CreditLimit))
                .WithDepositDays(AnsiConsole.Ask<int>(BankCreatingMessages.DepositDays))
                .WithDepositPercentages(dp).Build();
        }
        catch (ExistenceException exception)
        {
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"[red]{BankCreatingMessages.NotCreated()}[/] {exception.Message}");
            return null;
        }
    }

    private DepositPercentages? CreatePairs()
    {
        AnsiConsole.Clear();
        IPairsBuilder builder = DepositPercentages.Builder;
        int count = AnsiConsole.Ask<int>(BankCreatingMessages.DepositPairs);
        if (count < 0)
        {
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
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"[red]{BankCreatingMessages.NotCreated()}[/] {exception.Message}");
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
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"[red]{BankCreatingMessages.NotCreated()}[/] {exception.Message}");
            return null;
        }
    }
}