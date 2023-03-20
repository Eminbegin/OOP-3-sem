using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeClientHandlers;

public class AddEmailClientHandler : ChangeClientHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 3)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int clientId = AnsiConsole.Ask<int>(ClientChangeMessages.Client);
            try
            {
                cb.IsClientExists(clientId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadAddEmail()} {exception.Message}");
                return;
            }

            try
            {
                cb.AddEmailForMessages(clientId);
            }
            catch (ClientBuilderExceptions exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadAddEmail()} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, ClientChangeMessages.GoodAddEmail(clientId));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}