namespace EvoNet.Core.Neurons.Output;

public class MoveLeftOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, Agent agent, float value)
    {
        var pos = world.GetAgentPosition(agent);

        if (pos.X == 0)
        {
            return;
        }
        
        world.SetAgentPosition(agent, (pos.X - 1, pos.Y));
    }
}
