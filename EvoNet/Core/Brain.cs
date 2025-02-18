using System.Collections.Frozen;
using EvoNet.Core.Neurons;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;

namespace EvoNet.Core;

public class Brain
{
    private const byte NEURON_ID_MASK = 0b00111111;

    private const byte INPUT_NEURON_PREFIX = 0;

    private const byte INTERNAL_NEURON_PREFIX = 1;

    private const byte OUTPUT_NEURON_PREFIX = 2;

    public FrozenDictionary<byte, IInputNeuron> InputNeurons { get; init; }
    
    public FrozenDictionary<byte, InternalNeuron> InternalNeurons { get; init; }
    
    public FrozenDictionary<byte, IOutputNeuron> OutputNeurons { get; init; }

    public byte GetNeuronId(INeuron neuron)
    {
        (byte prefix, byte neuronId) = neuron switch
        {
            IInputNeuron => (INPUT_NEURON_PREFIX, InputNeurons.First(x => x.Value == neuron).Key),
            InternalNeuron => (INTERNAL_NEURON_PREFIX, InternalNeurons.First(x => x.Value == neuron).Key),
            IOutputNeuron => (OUTPUT_NEURON_PREFIX, OutputNeurons.First(x => x.Value == neuron).Key),
        };

        return (byte)(prefix << 6 | neuronId);
    }
    
    public INeuron GetNeuron(byte id)
    {
        byte neuronId = (byte)(id & NEURON_ID_MASK);

        return (id & ~NEURON_ID_MASK) switch
        {
            INPUT_NEURON_PREFIX => InputNeurons[neuronId],
            INTERNAL_NEURON_PREFIX => InternalNeurons[neuronId],
            OUTPUT_NEURON_PREFIX => OutputNeurons[neuronId],
            _ => throw new InvalidOperationException()
        };
    }
}
