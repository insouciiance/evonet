namespace EvoNet.Core.Neurons.Output;

public interface IOutputNeuron : INeuron
{
    void Process(World world, Agent agent, float value);
}
