namespace EvoNet.Core.Neurons.Output;

public class MoveRightOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, Agent agent, float value)
    {
        var pos = world.GetAgentPosition(agent);

        if (pos.X == world.Width - 1)
        {
            return;
        }
        
        world.SetAgentPosition(agent, (pos.X + 1, pos.Y));
    }
}
