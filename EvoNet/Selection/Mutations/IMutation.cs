using EvoNet.Core;

namespace EvoNet.Selection.Mutations;

public interface IMutation
{
    Agent Mutate(Agent agent);
}
