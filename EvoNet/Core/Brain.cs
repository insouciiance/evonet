using System.Collections.Frozen;
using EvoNet.Core.Neurons;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;

namespace EvoNet.Core;

public class Brain
{
    private const byte NEURON_ID_MASK = 0b00111111;
    
    public FrozenDictionary<byte, IInputNeuron> InputNeurons { get; init; }
    
    public FrozenDictionary<byte, InternalNeuron> InternalNeurons { get; init; }
    
    public FrozenDictionary<byte, IOutputNeuron> OutputNeurons { get; init; }

    public INeuron GetNeuron(byte id)
    {
        byte neuronId = (byte)(id & NEURON_ID_MASK);

        return (id & ~NEURON_ID_MASK) switch
        {
            0 => InputNeurons[neuronId],
            1 => InternalNeurons[neuronId],
            2 => OutputNeurons[neuronId],
            _ => throw new InvalidOperationException()
        };
    }
}
