namespace EvoNet.Core;

public class World
{
    public int Width { get; init; }
    
    public int Height { get; init; }
    
    public Agent[] Agents { get; init; }
    
    public Brain Brain { get; init; }

    public void MoveNext()
    {
        
    }

    private void ProcessAgent(Agent agent)
    {
        
    }
}
