namespace Isu.Extra.Entities;

public class Teacher
{
    public Teacher(string name, int isuId)
    {
        Name = name;
        IsuId = isuId;
    }

    public string Name { get; }
    public int IsuId { get; }
}