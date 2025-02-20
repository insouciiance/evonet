namespace EvoNet.Core;

public class World
{
    public int Width { get; init; }
    
    public int Height { get; init; }
    
    public Brain Brain { get; init; }
    
    public WorldAgent[] Agents { get; private set; }

    public void SetAgents(Agent[] agents)
    {
        Agents = new WorldAgent[agents.Length];
        
        for (int i = 0; i < Agents.Length; i++)
        {
            var agent = agents[i];
            
            Agents[i] = new()
            {
                Agent = agent,
                X = Random.Shared.Next(Width),
                Y = Random.Shared.Next(Height)
            };
        }
    }
}
