using DataAccess.Models.SendingMethods;

namespace DataAccess.Models.Workers;

public abstract class Worker
{
    protected Worker(Guid id, string name, Account account)
    {
        Id = id;
        Name = name;
        Account = account;
        SendingMethods = new List<SendingMethod>();
    }

    protected Worker() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Account Account { get; set;  }
    public virtual ICollection<SendingMethod> SendingMethods { get; set;  }
}