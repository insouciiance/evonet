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
            Brain = new(
                NeuronHelper.InputNeurons,
                new Dictionary<byte, InternalNeuron>()
                {
                    [1] = new(),
                    [2] = new()
                }.ToFrozenDictionary(),
                NeuronHelper.OutputNeurons)
        };

        var agents = GenerateAgents(world);
        
        world.SetAgents(agents);

        return world;
    }

    private Agent[] GenerateAgents(World world)
    {
        Agent[] agents = new Agent[PopulationSize];
        
        for (int i = 0; i < PopulationSize; i++)
        {
            var agent = GenerateAgent(world);
            agents[i] = agent;
        }

        return agents;
    }

    private Agent GenerateAgent(World world)
    {
        HashSet<byte> sourceIds = [];
        HashSet<byte> targetIds = [];

        Gene[] genes = new Gene[GeneCount];

        for (int j = 0; j < GeneCount; j++)
        {
            Gene gene;

            do
            {
                gene = GenerateGene();
            } while (gene.Source >= gene.Target);
            
            sourceIds.Add(gene.Source);
            targetIds.Add(gene.Target);
            
            genes[j] = gene;
        }

        Agent agent = new()
        {
            Genome = new(genes)
        };

        return agent;

        Gene GenerateGene()
        {
            if (sourceIds.Count == 0 && targetIds.Count == 0)
            {
                return new()
                {
                    Source = world.Brain.GetNeuronId(RandomSourceNeuron()),
                    Target = world.Brain.GetNeuronId(RandomTargetNeuron()),
                    ShortWeight = RandomWeight()
                };
            }

            return Random.Shared.Next(0, 2) switch
            {
                0 => new()
                {
                    Source = sourceIds.ElementAt(Random.Shared.Next(0, sourceIds.Count)),
                    Target = world.Brain.GetNeuronId(RandomTargetNeuron()),
                    ShortWeight = RandomWeight()
                },
                1 => new()
                {
                    Source = world.Brain.GetNeuronId(RandomSourceNeuron()),
                    Target = targetIds.ElementAt(Random.Shared.Next(0, targetIds.Count)),
                    ShortWeight = RandomWeight()
                }
            };

            short RandomWeight()
            {
                return (short)Random.Shared.Next(short.MinValue, short.MaxValue + 1);
            }
        }
        
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
