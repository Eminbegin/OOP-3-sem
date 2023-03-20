namespace Banks.Clients;

public interface IMessageSender
{
    Task SendMessage(string message);
}