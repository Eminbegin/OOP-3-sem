using Backups.Entities;

namespace Backups.Extra.Selectors;

public class AnySelector : IFusionSelectors
{
    public List<RestorePoint> SelectPoints(List<RestorePoint> points, List<ISelector> selectors)
    {
        IEnumerable<RestorePoint> result = points;
        foreach (ISelector selector in selectors)
        {
            result = result.Union(selector.SelectPoints(points));
        }

        return result.ToList();
    }
}