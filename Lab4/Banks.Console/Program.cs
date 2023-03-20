using Banks.BanksSystem;
using Banks.Console.Commands;
using Banks.Console.Handlers.StartHandlers;
namespace Banks.Console;

public static class Program
{
    private static void Main()
    {
        ICentralBank cb = new CentralBank();
        List<string> commands = StartCommandsRus.StartCommands;
        var dependencies = new HandlerDependencies();
        StartHandler firstHandler = dependencies.StartHandlerFirst;
        firstHandler.HandleRequest(-1, "F");
    }
}