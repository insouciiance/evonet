using EvoNet.Core;

namespace EvoNet.Selection.Crossovers;

public interface ICrossover
{
    Agent Produce(Agent x, Agent y);
}
