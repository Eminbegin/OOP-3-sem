namespace DataAccess.Models.SendingMethods;

public abstract class SendingMethod
{
    protected SendingMethod(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    protected SendingMethod() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
}