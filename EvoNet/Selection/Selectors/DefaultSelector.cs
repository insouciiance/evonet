using EvoNet.Core;

namespace EvoNet.Selection.Selectors;

public class DefaultSelector : ISelector
{
    public bool Select(Agent agent, World world)
    {
        return world.GetAgentPosition(agent).X >= world.Width / 2;
    }
}
