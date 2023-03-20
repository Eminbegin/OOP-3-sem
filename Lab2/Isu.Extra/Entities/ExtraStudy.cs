using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class ExtraStudy // joint group direction of training
{
    private List<ExtraStudyDivision> _divisions;

    public ExtraStudy(string name, MegaFaculty megaFaculty)
    {
        Name = name;
        MegaFaculty = megaFaculty;
        _divisions = new List<ExtraStudyDivision>();
    }

    public string Name { get; }
    public IReadOnlyCollection<ExtraStudyDivision> Divisions => _divisions;
    public MegaFaculty MegaFaculty { get; }

    public void AddDivision(ExtraStudyDivision extraStudyDivision)
    {
        if (_divisions.Any(d => d.Equals(extraStudyDivision)))
        {
            throw InvalidExtraStudyException.DivisionAlreadyExist(extraStudyDivision);
        }

        _divisions.Add(extraStudyDivision);
    }
}