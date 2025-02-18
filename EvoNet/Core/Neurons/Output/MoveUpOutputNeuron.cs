namespace EvoNet.Core.Neurons.Output;

public class MoveUpOutputNeuron : IOutputNeuron
{
    public bool ShouldActivate(float value) => value >= 0;

    public void Activate(World world, Agent agent, float value)
    {
        ref var pos = ref world.GetAgentPositionRef(agent);

        if (pos.Y == 0)
        {
            return;
        }
        
        pos.Y -= 1;
    }
}
