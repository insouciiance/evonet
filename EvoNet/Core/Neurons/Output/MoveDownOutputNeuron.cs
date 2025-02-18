namespace EvoNet.Core.Neurons.Output;

public class MoveDownOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, Agent agent, float value)
    {
        var pos = world.GetAgentPosition(agent);

        if (pos.Y == world.Height - 1)
        {
            return;
        }
        
        world.SetAgentPosition(agent, (pos.X, pos.Y + 1));
    }
}
