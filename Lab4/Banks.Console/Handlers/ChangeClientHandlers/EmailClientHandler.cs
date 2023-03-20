using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeClientHandlers;

public class EmailClientHandler : ChangeClientHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 2)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int clientId = AnsiConsole.Ask<int>(ClientChangeMessages.Client);
            try
            {
                cb.IsClientExists(clientId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadEmail()} {exception.Message}");
                return;
            }

            string email = AnsiConsole.Ask<string>(ClientChangeMessages.Email);
            try
            {
                cb.SetEmail(clientId, email);
            }
            catch (ClientBuilderExceptions exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadEmail()} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, ClientChangeMessages.GoodEmail(clientId));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}