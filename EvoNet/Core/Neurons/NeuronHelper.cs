using System.Collections.Frozen;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;

namespace EvoNet.Core.Neurons;

public static class NeuronHelper
{
    public static readonly FrozenDictionary<byte, IInputNeuron> InputNeurons = new Dictionary<byte, IInputNeuron>()
    {
        [1] = new HorizontalPositionInputNeuron(),
        [2] = new VerticalPositionInputNeuron()
    }.ToFrozenDictionary();
    
    public static readonly FrozenDictionary<byte, IOutputNeuron> OutputNeurons = new Dictionary<byte, IOutputNeuron>()
    {
        [1] = new MoveLeftOutputNeuron(),
        [2] = new MoveRightOutputNeuron(),
        [3] = new MoveUpOutputNeuron(),
        [4] = new MoveDownOutputNeuron()
    }.ToFrozenDictionary();
}
