using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Spectre.Console;

namespace Banks.Console.Handlers.CreatingHandlers;

public class PersonCreatingHandler : CreatingHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 0)
        {
            AnsiConsole.Clear();
            string name = AnsiConsole.Ask<string>(PersonCreatingMessages.Name);
            string surname = AnsiConsole.Ask<string>(PersonCreatingMessages.Surname);
            int id = CentralBank.GetInstance().AddPerson(name, surname);
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, PersonCreatingMessages.GetMessage(name, surname, id));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}