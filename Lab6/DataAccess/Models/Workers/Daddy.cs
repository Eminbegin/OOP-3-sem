namespace DataAccess.Models.Workers;

public class Daddy : Worker
{
    public Daddy(Guid id, string name, Account account, Area area)
        : base(id, name, account)
    {
        Area = area;
    }

    protected Daddy() { }

    public virtual Area Area { get; set; }
}