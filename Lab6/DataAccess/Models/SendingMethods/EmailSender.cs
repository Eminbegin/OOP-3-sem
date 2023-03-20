namespace DataAccess.Models.SendingMethods;

public class EmailSender : SendingMethod
{
    public EmailSender(Guid id, string name)
        : base(id, name)
    {
    }

    public EmailSender()
    {
    }
}