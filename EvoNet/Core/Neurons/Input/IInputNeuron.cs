namespace EvoNet.Core.Neurons.Input;

public interface IInputNeuron : INeuron
{
    float Process(World world, Agent agent);
}
