using EvoNet.Core;
using EvoNet.Core.Neurons;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;
using EvoNet.Selection;
using EvoNet.Utils;

namespace EvoNet.Simulation;

public class SimulationRunner
{
    public World World { get; init; }
    
    public NaturalSelection NaturalSelection { get; init; }
    
    public SimulationConfig Configuration { get; init; }

    public event Action<World, int, int>? StepFinished;
    
    public event Action<World, int>? GenerationFinished;

    public void Run()
    {
        for (int i = 0; i < Configuration.GenerationCount; i++)
        {
            for (int j = 0; j < Configuration.StepsPerGeneration; j++)
            {
                MoveNext();
                StepFinished?.Invoke(World, i, j);
            }
            
            GenerationFinished?.Invoke(World, i);
            NaturalSelection.RunOnce(World);
        }
    }
    
    private void MoveNext()
    {
        foreach (var agent in World.Agents)
        {
            ProcessAgent(agent);
        }
    }

    private void ProcessAgent(Agent agent)
    {
        Span<float> neuronValues = stackalloc float[byte.MaxValue];
        
        foreach (byte id in agent.Genome.OrderedNeurons)
        {
            var neuron = World.Brain.GetNeuron(id);
            ProcessNeuron(id, neuron, neuronValues);
        }

        void ProcessNeuron(byte id, INeuron neuron, Span<float> neuronValues)
        {
            var prefix = World.Brain.GetNeuronPrefix(id);
            
            if (prefix == Brain.INPUT_NEURON_PREFIX)
            {
                var inputNeuron = (IInputNeuron)neuron;
                
                float value = inputNeuron.Process(World, agent);
                neuronValues[id] = value;

                PropagateValue(value, neuronValues);
                
                return;
            }

            if (prefix == Brain.INTERNAL_NEURON_PREFIX)
            {
                float value = neuronValues[id];
                value = MathHelper.Tanh(value);
                
                PropagateValue(value, neuronValues);
                
                return;
            }

            if (prefix == Brain.OUTPUT_NEURON_PREFIX)
            {
                float value = neuronValues[id];
                value = MathHelper.Tanh(value);

                var outputNeuron = (IOutputNeuron)neuron;
                
                if (outputNeuron.ShouldActivate(value))
                {
                    outputNeuron.Activate(World, agent, value);
                }
            }

            void PropagateValue(float value, Span<float> neuronValues)
            {
                if (agent.Genome.Graph[id] is not { } outgoing)
                {
                    return;
                }
                
                foreach (var gene in outgoing)
                {
                    neuronValues[gene.Target] += value * gene.Weight;
                }
            }
        }
    }
}
