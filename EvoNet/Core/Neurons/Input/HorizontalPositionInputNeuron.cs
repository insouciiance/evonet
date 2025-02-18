namespace EvoNet.Core.Neurons.Input;

public class HorizontalPositionInputNeuron : IInputNeuron
{
    public float Process(World world, Agent agent)
    {
        var pos = world.GetAgentPosition(agent);
        return (float)pos.X / world.Width;
    }
}
