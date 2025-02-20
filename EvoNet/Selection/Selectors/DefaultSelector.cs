using EvoNet.Core;

namespace EvoNet.Selection.Selectors;

public class DefaultSelector : ISelector
{
    public bool Select(WorldAgent agent, World world)
    {
        return agent.X >= world.Width / 2;
    }
}
