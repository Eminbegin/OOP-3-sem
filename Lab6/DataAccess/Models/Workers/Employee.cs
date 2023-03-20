namespace DataAccess.Models.Workers;

public class Employee : Worker
{
    public Employee(Account account, Guid id, string name, Group group)
        : base(id, name, account)
    {
        Id = id;
        Name = name;
        Account = account;
        Group = group;
    }

    protected Employee() { }

    public virtual Group Group { get; set; }
                                                                                                                                                                                                                                }