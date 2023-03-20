using Isu.Entities;

namespace Isu.Extra.Entities;

public class ExtraGroup
{
    public ExtraGroup(Group group)
    {
        Group = group;
        Schedule = Schedule.Empty;
    }

    public Group Group { get; }
    public Schedule Schedule { get; private set; }

    public void ChangeSchedule(Schedule schedule)
    {
        Schedule = schedule;
    }
}