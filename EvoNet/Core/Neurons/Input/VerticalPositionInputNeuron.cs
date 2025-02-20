namespace EvoNet.Core.Neurons.Input;

public class VerticalPositionInputNeuron : IInputNeuron
{
    public float Process(World world, WorldAgent agent)
    {
        return (float)agent.Y / world.Height;
    }
}
