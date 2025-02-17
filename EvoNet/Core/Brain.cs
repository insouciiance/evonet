using System.Collections.Frozen;
using EvoNet.Core.Neurons;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;

namespace EvoNet.Core;

public class Brain
{
    private const byte NEURON_ID_MASK = 0b01111111;
    
    public FrozenDictionary<byte, IInputNeuron> InputNeurons { get; init; }
    
    public FrozenDictionary<byte, InternalNeuron> InternalNeurons { get; init; }
    
    public FrozenDictionary<byte, IOutputNeuron> OutputNeurons { get; init; }

    public INeuron GetSourceNeuron(byte id)
    {
        byte neuronId = (byte)(id & NEURON_ID_MASK);

        if ((id & ~NEURON_ID_MASK) == 0)
        {
            return InputNeurons[neuronId];
        }
        
        return InternalNeurons[neuronId];
    }

    public INeuron GetTargetNeuron(byte id)
    {
        byte neuronId = (byte)(id & NEURON_ID_MASK);

        if ((id & ~NEURON_ID_MASK) == 0)
        {
            return OutputNeurons[neuronId];
        }
        
        return InternalNeurons[neuronId];
    }
}
