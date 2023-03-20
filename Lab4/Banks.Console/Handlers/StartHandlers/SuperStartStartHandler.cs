using Banks.Console.Commands;
using Spectre.Console;

namespace Banks.Console.Handlers.StartHandlers;

public class SuperStartStartHandler : StartHandler
{
    public override void HandleRequest(int condition, string text)
    {
        if (condition == -1)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine(text);
            List<string> commands = StartCommandsRus.StartCommands;
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Что ты тут забыл?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Там ниже что-то ещё есть сто проц)[/]")
                    .AddChoices(commands));

            Successor?.HandleRequest(commands.IndexOf(command), text);
        }
        else
        {
            Successor?.HandleRequest(condition, text);
        }
    }
}