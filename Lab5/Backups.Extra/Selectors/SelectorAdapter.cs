using Backups.Entities;

namespace Backups.Extra.Selectors;

public class SelectorAdapter : ISelector
{
    private readonly IFusionSelectors _fusion;
    private readonly List<ISelector> _selectors;

    public SelectorAdapter(IFusionSelectors fusion, List<ISelector> selectors)
    {
        _fusion = fusion;
        _selectors = selectors;
    }

    public List<RestorePoint> SelectPoints(List<RestorePoint> points)
    {
        return _fusion.SelectPoints(points, _selectors);
    }
}