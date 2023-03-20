using Banks.BanksSystem;
using Banks.Console.Commands;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.CreatingHandlers;

public class AccountCreatingHandler : CreatingHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 3)
        {
            AnsiConsole.Clear();
            ICentralBank cb = CentralBank.GetInstance();
            int clientId = AnsiConsole.Ask<int>(AccountCreationMessages.ClientId);
            try
            {
                cb.IsClientExists(clientId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{AccountCreationMessages.WithException()} {exception.Message}");
                return;
            }

            List<string> commands = AccountsCreationCommands.Commands;
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Что ты тут забыл?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Там ниже что-то ещё есть сто проц)[/]")
                    .AddChoices(commands));
            HandlerDependencies.GetInstance().AccountCreatingFirst.HandleRequest(commands.IndexOf(command),  clientId);
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}