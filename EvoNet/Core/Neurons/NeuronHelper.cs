using System.Collections.Frozen;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;

namespace EvoNet.Core.Neurons;

public static class NeuronHelper
{
    public static readonly FrozenDictionary<byte, IInputNeuron> InputNeurons = new Dictionary<byte, IInputNeuron>()
    {
        
    }.ToFrozenDictionary();
    
    public static readonly FrozenDictionary<byte, IOutputNeuron> OutputNeurons = new Dictionary<byte, IOutputNeuron>()
    {
        
    }.ToFrozenDictionary();
}
