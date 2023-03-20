using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeClientHandlers;

public class AddressClientHandler : ChangeClientHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 1)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int clientId = AnsiConsole.Ask<int>(ClientChangeMessages.Client);
            try
            {
                cb.IsClientExists(clientId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadAddress()} {exception.Message}");
                return;
            }

            string address = AnsiConsole.Ask<string>(ClientChangeMessages.Address);
            try
            {
                cb.SetAddress(clientId, address);
            }
            catch (ClientBuilderExceptions exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadAddress()} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, ClientChangeMessages.GoodAddress(clientId));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}