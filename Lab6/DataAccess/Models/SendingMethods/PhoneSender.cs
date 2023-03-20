namespace DataAccess.Models.SendingMethods;

public class PhoneSender : SendingMethod
{
    public PhoneSender(Guid id, string name)
        : base(id, name)
    {
    }

    public PhoneSender()
    {
    }
}