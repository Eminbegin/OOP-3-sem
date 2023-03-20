namespace DataAccess.Models.Messages;

public class PhoneMessage : AbstractMessage
{
    public PhoneMessage(Guid id, string text, MessageState state, Guid recipient, DateTime sendingTime, string phoneNumber)
        : base(id, text, state, recipient, sendingTime)
    {
        PhoneNumber = phoneNumber;
    }

    protected PhoneMessage() { }

    public string PhoneNumber { get; set; }
}