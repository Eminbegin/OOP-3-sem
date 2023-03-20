using Backups.Entities;

namespace Backups.Extra.Selectors;

public interface ISelector
{
    List<RestorePoint> SelectPoints(List<RestorePoint> points);
}