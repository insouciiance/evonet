using EvoNet.Core;
using EvoNet.Core.Neurons;
using EvoNet.Core.Neurons.Input;
using EvoNet.Core.Neurons.Output;
using EvoNet.Selection;

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
        Dictionary<byte, float> neuronValues = [];

        foreach (var id in agent.Genome.OrderedNeurons)
        {
            neuronValues[id] = 0;
        }
        
        foreach (byte id in agent.Genome.OrderedNeurons)
        {
            var neuron = World.Brain.GetNeuron(id);
            ProcessNeuron(id, neuron);
        }

        void ProcessNeuron(byte id, INeuron neuron)
        {
            if (neuron is IInputNeuron inputNeuron)
            {
                float value = inputNeuron.Process(World, agent);
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
                value = MathF.Tanh(value);

                if (outputNeuron.ShouldActivate(value))
                {
                    outputNeuron.Activate(World, agent, value);
                }
            }

            void PropagateValue(float value)
            {
                if (!agent.Genome.Graph.TryGetValue(id, out var outgoing))
                {
                    return;
                }
                
                foreach (var gene in outgoing)
                {
                    neuronValues[gene.Target] += value * gene.GetWeight();
                }
            }
        }
    }
}
