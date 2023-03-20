using Banks.Console.Commands;
using Spectre.Console;

namespace Banks.Console.Handlers.StartHandlers;

public class DoSmthStartHandler : StartHandler
{
    public override void HandleRequest(int condition, string text)
    {
        if (condition == 1)
        {
            List<string> commands = ActionCommands.Commands;
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Что ты тут забыл?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Там ниже что-то ещё есть сто проц)[/]")
                    .AddChoices(commands));
            AnsiConsole.Clear();
            HandlerDependencies.GetInstance().ActionHandlerFirst.HandleRequest(commands.IndexOf(command));
        }
        else
        {
            Successor?.HandleRequest(condition, text);
        }
    }
}