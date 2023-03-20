using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeClientHandlers;

public class PassportClientHandler : ChangeClientHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 0)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int clientId = AnsiConsole.Ask<int>(ClientChangeMessages.Client);
            try
            {
                cb.IsClientExists(clientId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadPassport()} {exception.Message}");
                return;
            }

            string passport = AnsiConsole.Ask<string>(ClientChangeMessages.Passport);
            try
            {
                cb.SetPassport(clientId, passport);
            }
            catch (ClientBuilderExceptions exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadPassport()} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, ClientChangeMessages.GoodPassport(clientId));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}