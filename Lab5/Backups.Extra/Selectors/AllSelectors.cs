using Backups.Entities;

namespace Backups.Extra.Selectors;

public class AllSelectors : IFusionSelectors
{
    public List<RestorePoint> SelectPoints(List<RestorePoint> points, List<ISelector> selectors)
    {
        IEnumerable<RestorePoint> result = points;
        foreach (ISelector selector in selectors)
        {
            result = result.Intersect(selector.SelectPoints(points));
        }

        return result.ToList();
    }
}