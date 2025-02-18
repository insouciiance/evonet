namespace EvoNet.Core.Neurons.Input;

public class VerticalPositionInputNeuron : IInputNeuron
{
    public float Process(World world, Agent agent)
    {
        var pos = world.GetAgentPosition(agent);
        return (float)pos.Y / world.Height;
    }
}
