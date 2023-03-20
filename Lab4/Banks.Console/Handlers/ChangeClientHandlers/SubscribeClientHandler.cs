using Banks.BanksSystem;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.ChangeClientHandlers;

public class SubscribeClientHandler : ChangeClientHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 4)
        {
            ICentralBank cb = CentralBank.GetInstance();
            int clientId = AnsiConsole.Ask<int>(ClientChangeMessages.Client);
            try
            {
                cb.IsClientExists(clientId);
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientChangeMessages.BadSubscribe()} {exception.Message}");
                return;
            }

            cb.SubscribeClientToNotifications(clientId);
            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, ClientChangeMessages.GoodSubscribe(clientId));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }
}