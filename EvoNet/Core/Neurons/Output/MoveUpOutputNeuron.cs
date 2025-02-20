namespace EvoNet.Core.Neurons.Output;

public class MoveUpOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, WorldAgent agent, float value)
    {
        if (agent.Y == 0)
        {
            return;
        }
        
        agent.Y -= 1;
    }
}
