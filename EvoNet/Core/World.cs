using Vector2I = (int X, int Y);

namespace EvoNet.Core;

public class World
{
    private readonly Dictionary<Agent, Vector2I> _agentPositions = [];
    
    public int Width { get; init; }
    
    public int Height { get; init; }
    
    public Brain Brain { get; init; }
    
    public Agent[] Agents { get; private set; }

    public void SetAgents(Agent[] agents)
    {
        _agentPositions.Clear();
        
        Agents = agents;

        foreach (var agent in Agents)
        {
            _agentPositions[agent] = (Random.Shared.Next(Width), Random.Shared.Next(Height));
        }
    }

    public Vector2I GetAgentPosition(Agent agent)
    {
        return _agentPositions[agent];
    }

    public void SetAgentPosition(Agent agent, Vector2I position)
    {
        _agentPositions[agent] = position;
    }
}
