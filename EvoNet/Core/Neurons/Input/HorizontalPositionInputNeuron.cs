namespace EvoNet.Core.Neurons.Input;

public class HorizontalPositionInputNeuron : IInputNeuron
{
    public float Process(World world, WorldAgent agent)
    {
        return (float)agent.X / world.Width;
    }
}
