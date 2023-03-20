using Backups.Entities;

namespace Backups.Extra.Selectors;

public interface IFusionSelectors
{
    List<RestorePoint> SelectPoints(List<RestorePoint> points, List<ISelector> selectors);
}