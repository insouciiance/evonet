using System.Collections.Frozen;
using EvoNet.Core;
using EvoNet.Core.Neurons;

namespace EvoNet.Generation;

public class DefaultWorldGenerator : IWorldGenerator
{
    public int PopulationSize { get; init; }
    
    public int GeneCount { get; init; }
    
    public World Generate()
    {
        World world = new()
        {
            Width = 128,
            Height = 128,
            Brain = new()
            {
                InputNeurons = NeuronHelper.InputNeurons,
                InternalNeurons = new Dictionary<byte, InternalNeuron>()
                {
                    [1] = new(),
                    [2] = new()
                }.ToFrozenDictionary(),
                OutputNeurons = NeuronHelper.OutputNeurons,
            }
        };

        Agent[] agents = new Agent[PopulationSize];
        
        for (int i = 0; i < PopulationSize; i++)
        {
            Gene[] genes = new Gene[GeneCount];

            for (int j = 0; j < GeneCount; j++)
            {
                var source = RandomSourceNeuron();
                var target = RandomTargetNeuron();
                
                Gene gene = new()
                {
                    Source = world.Brain.GetNeuronId(source),
                    Target = world.Brain.GetNeuronId(target),
                    Weight = (short)Random.Shared.Next(short.MinValue, short.MaxValue + 1),
                };
                
                genes[j] = gene;
            }

            Agent agent = new()
            {
                Genome = new(genes)
            };
            
            agents[i] = agent;
        }
        
        world.SetAgents(agents);

        return world;

        INeuron RandomSourceNeuron()
        {
            return Random.Shared.NextSingle() switch
            {
                <= 0.5f => world.Brain.InputNeurons.ElementAt(Random.Shared.Next(world.Brain.InputNeurons.Count)).Value,
                _ => world.Brain.InternalNeurons.ElementAt(Random.Shared.Next(world.Brain.InternalNeurons.Count)).Value,
            };
        }

        INeuron RandomTargetNeuron()
        {
            return Random.Shared.NextSingle() switch
            {
                <= 0.5f => world.Brain.InternalNeurons.ElementAt(Random.Shared.Next(world.Brain.InternalNeurons.Count)).Value,
                _ => world.Brain.OutputNeurons.ElementAt(Random.Shared.Next(world.Brain.OutputNeurons.Count)).Value,
            };
        }
    }
}
