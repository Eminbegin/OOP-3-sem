namespace DataAccess.Models.Messages;

public class MessengerMessage : AbstractMessage
{
    public MessengerMessage(Guid id, string text, MessageState state, Guid recipient, DateTime sendingTime, string userTag)
        : base(id, text, state, recipient, sendingTime)
    {
        UserTag = userTag;
    }

    protected MessengerMessage() { }

    public string UserTag { get; set; }
}