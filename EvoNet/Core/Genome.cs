using System.Runtime.InteropServices;
using EvoNet.Utils;

namespace EvoNet.Core;

public class Genome
{
    public readonly Gene[] Genes;

    public readonly Dictionary<byte, List<Gene>> Graph;
    
    public readonly byte[] OrderedNeurons;

    public Genome(Gene[] genes)
    {
        Genes = genes;

        Graph = InitializeGraph(Genes);
        
        Span<byte> orderedNeurons = stackalloc byte[Genes.Length * 2];
        
        int neuronsCount = TopologicalSorting.Sort(Genes, Graph, orderedNeurons);
        
        OrderedNeurons = orderedNeurons[..neuronsCount].ToArray();
    }
    
    private static Dictionary<byte, List<Gene>> InitializeGraph(Gene[] genes)
    {
        Dictionary<byte, List<Gene>> graph = [];

        foreach (var gene in genes)
        {
            ref var to = ref CollectionsMarshal.GetValueRefOrAddDefault(graph, gene.Source, out bool exists);

            if (!exists)
            {
                to = new();
            }
            
            to!.Add(gene);
        }

        return graph;
    }
}
