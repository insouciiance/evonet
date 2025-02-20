namespace EvoNet.Core.Neurons.Output;

public class MoveRightOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, WorldAgent agent, float value)
    {
        if (agent.X == world.Width - 1)
        {
            return;
        }
        
        agent.X += 1;
    }
}
