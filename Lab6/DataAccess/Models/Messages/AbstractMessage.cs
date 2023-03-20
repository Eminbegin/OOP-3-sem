namespace DataAccess.Models.Messages;

public abstract class AbstractMessage
{
    public AbstractMessage(Guid id, string text, MessageState state, Guid recipient, DateTime sendingTime)
    {
        Id = id;
        Text = text;
        State = state;
        Recipient = recipient;
        SendingTime = sendingTime;
    }

    protected AbstractMessage() { }

    public Guid Id { get; set; }
    public Guid Recipient { get; set; }
    public string Text { get; set; }
    public MessageState State { get; set; }
    public DateTime SendingTime { get; set; }
                                                                                                                                                                                    }