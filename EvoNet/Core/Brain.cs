using System.Collections.Frozen;
using EvoNet.Core.Neurons;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;

namespace EvoNet.Core;

public class Brain
{
    public const byte INPUT_NEURON_PREFIX = 0;

    public const byte INTERNAL_NEURON_PREFIX = 1;

    public const byte OUTPUT_NEURON_PREFIX = 2;

    public FrozenDictionary<byte, IInputNeuron> InputNeurons { get; }
    
    public FrozenDictionary<byte, InternalNeuron> InternalNeurons { get; }
    
    public FrozenDictionary<byte, IOutputNeuron> OutputNeurons { get; }

    private readonly INeuron[] _neuronsIndex = new INeuron[byte.MaxValue];
    
    public Brain(
        FrozenDictionary<byte, IInputNeuron> inputNeurons,
        FrozenDictionary<byte, InternalNeuron> internalNeurons,
        FrozenDictionary<byte, IOutputNeuron> outputNeurons)
    {
        InputNeurons = inputNeurons;
        InternalNeurons = internalNeurons;
        OutputNeurons = outputNeurons;

        foreach (var (id, neuron) in inputNeurons)
        {
            byte neuronId = BuildNeuronId(INPUT_NEURON_PREFIX, id);
            _neuronsIndex[neuronId] = neuron;
        }
        
        foreach (var (id, neuron) in internalNeurons)
        {
            byte neuronId = BuildNeuronId(INTERNAL_NEURON_PREFIX, id);
            _neuronsIndex[neuronId] = neuron;
        }
        
        foreach (var (id, neuron) in outputNeurons)
        {
            byte neuronId = BuildNeuronId(OUTPUT_NEURON_PREFIX, id);
            _neuronsIndex[neuronId] = neuron;
        }
    }
    
    public byte GetNeuronId(INeuron neuron) => (byte)Array.IndexOf(_neuronsIndex, neuron);
    
    public INeuron GetNeuron(byte id) => _neuronsIndex[id];

    public byte GetNeuronPrefix(byte id) => (byte)(id >> 6);

    private byte BuildNeuronId(byte prefix, byte neuronId) => (byte)(prefix << 6 | neuronId);
}
