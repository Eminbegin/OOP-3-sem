namespace DataAccess.Models.SendingMethods;

public class MessengerSender : SendingMethod
{
    public MessengerSender(Guid id, string name)
        : base(id, name)
    {
    }

    public MessengerSender()
    {
    }
}