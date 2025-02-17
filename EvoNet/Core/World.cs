using System.Runtime.InteropServices;
using EvoNet.Core.Neurons;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;

namespace EvoNet.Core;

public class World
{
    public int Width { get; init; }
    
    public int Height { get; init; }
    
    public Agent[] Agents { get; init; }
    
    public Brain Brain { get; init; }

    public void MoveNext()
    {
        foreach (var agent in Agents)
        {
            ProcessAgent(agent);
        }
    }

    private void ProcessAgent(Agent agent)
    {
        Dictionary<byte, float> neuronValues = [];
        
        foreach (byte id in agent.Genome.OrderedNeurons)
        {
            var neuron = Brain.GetNeuron(id);
            ProcessNeuron(id, neuron);
        }

        void ProcessNeuron(byte id, INeuron neuron)
        {
            if (neuron is IInputNeuron inputNeuron)
            {
                float value = inputNeuron.Process(this, agent);
                neuronValues[id] = value;

                PropagateValue(value);
                
                return;
            }

            if (neuron is InternalNeuron)
            {
                float value = neuronValues[id];
                value = MathF.Tanh(value);
                
                PropagateValue(value);
                
                return;
            }

            if (neuron is IOutputNeuron outputNeuron)
            {
                float value = neuronValues[id];
                outputNeuron.Process(this, agent, value);
            }

            void PropagateValue(float value)
            {
                foreach (var gene in agent.Genome.Graph[id])
                {
                    neuronValues[gene.Target] += value * gene.GetWeight();
                }
            }
        }
    }
}
