using Banks.Console.Commands;
using Spectre.Console;

namespace Banks.Console.Handlers.StartHandlers;

public class CreateSmthStartHandler : StartHandler
{
    public override void HandleRequest(int condition, string text)
    {
        if (condition == 0)
        {
            List<string> commands = CreationCommands.Commands;
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Что ты тут забыл?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Там ниже что-то ещё есть сто проц)[/]")
                    .AddChoices(commands));
            HandlerDependencies.GetInstance().CreatingHandlerFirst.HandleRequest(commands.IndexOf(command));
        }
        else
        {
            Successor?.HandleRequest(condition, text);
        }
    }
}