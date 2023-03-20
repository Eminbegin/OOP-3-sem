using DataAccess.Models.SendingMethods;

namespace DataAccess.Models;

public class SendMethodMessages
{
    public SendMethodMessages(Guid id, SendingMethod sendingMethod, int count)
    {
        SendingMethod = sendingMethod;
        Count = count;
        Id = id;
    }

    protected SendMethodMessages() { }

    public virtual SendingMethod SendingMethod { get; set; }
    public Guid Id { get; set; }
    public int Count { get; set; }
}