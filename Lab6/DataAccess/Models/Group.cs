using DataAccess.Models.SendingMethods;
using DataAccess.Models.Workers;

namespace DataAccess.Models;

public class Group
{
    public Group(Area area, Guid id, string name)
    {
        Area = area;
        Id = id;
        Name = name;
        MiniDaddies = new List<Employee>();
        Employees = new List<Employee>();
        SendingMethods = new List<SendingMethod>();
    }

    protected Group() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Area Area { get; set; }
    public virtual ICollection<SendingMethod> SendingMethods { get; set; }
    public virtual ICollection<Employee> MiniDaddies { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}