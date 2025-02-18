using System.Runtime.InteropServices;
using EvoNet.Utils;

namespace EvoNet.Core;

public class Genome
{
    private Dictionary<byte, List<Gene>>? _graph;

    private byte[]? _orderedNeurons;
    
    public readonly Gene[] Genes;

    public Dictionary<byte, List<Gene>> Graph
    {
        get
        {
            _graph ??= InitializeGraph(Genes);
            return _graph;
        }
    }

    public byte[] OrderedNeurons
    {
        get
        {
            _orderedNeurons ??= InitializeOrderedNeurons(Genes, Graph);
            return _orderedNeurons;
        }
    }

    public Genome(Gene[] genes)
    {
        Genes = genes;

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

    private static byte[] InitializeOrderedNeurons(Gene[] genes, Dictionary<byte, List<Gene>> graph)
    {
        Span<byte> orderedNeurons = stackalloc byte[genes.Length * 2];
        
        int neuronsCount = TopologicalSorting.Sort(genes, graph, orderedNeurons);
        
        return orderedNeurons[..neuronsCount].ToArray();
    }
}
