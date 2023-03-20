namespace Banks.Clients;

public class Person : IEquatable<Person>
{
    private readonly List<IClient> _clients;

    public Person(string surname, string name, int id)
    {
        Surname = surname;
        Name = name;
        Id = id;
        _clients = new List<IClient>();
    }

    public string Name { get; }
    public string Surname { get; }
    public int Id { get; }

    public void AddClient(IClient client)
    {
        _clients.Add(client);
    }

    public bool Equals(Person? other)
    {
        return other is not null && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Person);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Surname);
    }
}