using DataAccess.Models.Workers;

namespace DataAccess.Models;

public class Area
{
    public Area(string name, Guid id)
    {
        Name = name;
        Groups = new List<Group>();
        Daddies = new List<Daddy>();
        Id = id;
    }

    protected Area() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Group> Groups { get; set; }
    public virtual ICollection<Daddy> Daddies { get; set; }
}