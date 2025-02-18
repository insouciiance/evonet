using EvoNet.Core;

namespace EvoNet.Selection.Mutations;

public class DefaultMutation : IMutation
{
    public Agent Mutate(Agent agent)
    {
        return agent;
    }
}
