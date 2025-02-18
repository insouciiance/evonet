namespace EvoNet.Core.Neurons.Output;

public class MoveLeftOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, Agent agent, float value)
    {
        ref var pos = ref world.GetAgentPositionRef(agent);

        if (pos.X == 0)
        {
            return;
        }
        
        pos.X -= 1;
    }
}
