namespace EvoNet.Core.Neurons.Output;

public interface IOutputNeuron : INeuron
{
    bool ShouldActivate(float value);
    
    void Activate(World world, Agent agent, float value);
}
