using EvoNet.Core;

namespace EvoNet.Selection.Crossovers;

public class DefaultCrossover : ICrossover
{
    public Agent Produce(Agent x, Agent y)
    {
        Gene[] xGenes = x.Genome.Genes;
        Gene[] yGenes = y.Genome.Genes;

        Gene[] childGenes = xGenes.Concat(yGenes)
            .Skip(Random.Shared.Next(xGenes.Length))
            .Take(xGenes.Length)
            .ToArray();
        
        Agent child = new()
        {
            Genome = new(childGenes)
        };
        
        return child;
    }
}
