using Banks.Console.Commands;
using Spectre.Console;

namespace Banks.Console.Handlers.StartHandlers;

public class ChangeBankStartHandler : StartHandler
{
    public override void HandleRequest(int condition, string text)
    {
        if (condition == 3)
        {
            List<string> commands = ChangeBanksCommands.Commands;
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Что ты тут забыл?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Там ниже что-то ещё есть сто проц)[/]")
                    .AddChoices(commands));
            HandlerDependencies.GetInstance().BankHandlerFirst.HandleRequest(commands.IndexOf(command));
        }
        else
        {
            Successor?.HandleRequest(condition, text);
        }
    }
}