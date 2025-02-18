using EvoNet.Core;

namespace EvoNet.Selection.Selectors;

public interface ISelector
{
    bool Select(Agent agent, World world);
}
