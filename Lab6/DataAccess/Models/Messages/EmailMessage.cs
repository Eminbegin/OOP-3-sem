namespace DataAccess.Models.Messages;

public class EmailMessage : AbstractMessage
{
    public EmailMessage(Guid id, string text, MessageState state, Guid recipient, DateTime sendingTime, string theme)
        : base(id, text, state, recipient, sendingTime)
    {
        Theme = theme;
    }

    protected EmailMessage() { }

    public string Theme { get; set; }
}