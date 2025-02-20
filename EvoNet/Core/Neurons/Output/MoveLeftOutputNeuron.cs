namespace EvoNet.Core.Neurons.Output;

public class MoveLeftOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, WorldAgent agent, float value)
    {
        if (agent.X == 0)
        {
            return;
        }
        
        agent.X -= 1;
    }
}
