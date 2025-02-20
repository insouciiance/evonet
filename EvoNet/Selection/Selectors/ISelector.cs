using EvoNet.Core;

namespace EvoNet.Selection.Selectors;

public interface ISelector
{
    bool Select(WorldAgent agent, World world);
}
