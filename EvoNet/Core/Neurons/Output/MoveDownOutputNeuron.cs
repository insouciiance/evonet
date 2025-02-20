namespace EvoNet.Core.Neurons.Output;

public class MoveDownOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, WorldAgent agent, float value)
    {
        if (agent.Y == world.Height - 1)
        {
            return;
        }
        
        agent.Y += 1;
    }
}
