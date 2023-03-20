namespace Isu.Extra.Models;

public class MegaFaculty
{
    public MegaFaculty(string name, string availableGroups)
    {
        Name = name;
        AvailableGroups = availableGroups;
    }

    public string Name { get; }
    public string AvailableGroups { get; }
}