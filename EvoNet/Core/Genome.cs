using System.Buffers;
using System.Runtime.InteropServices;
using EvoNet.Utils;

namespace EvoNet.Core;

public class Genome : IDisposable
{
    private List<Gene>[]? _graph;

    private byte[]? _orderedNeurons;

    private int _neuronsCount;
    
    public readonly Gene[] Genes;

    public List<Gene>[] Graph
    {
        get
        {
            _graph ??= InitializeGraph(Genes);
            return _graph;
        }
    }

    public Span<byte> OrderedNeurons
    {
        get
        {
            if (_orderedNeurons is null)
            {
                _orderedNeurons = InitializeOrderedNeurons(Genes, Graph, out int neuronsCount);
                _neuronsCount = neuronsCount;
            }

            return _orderedNeurons.AsSpan()[.._neuronsCount];
        }
    }

    public Genome(Gene[] genes)
    {
        Genes = genes;
    }
    
    public void Dispose()
    {
        if (_graph is not null)
        {
            ArrayPool<List<Gene>>.Shared.Return(_graph, true);
        }

        if (_orderedNeurons is not null)
        {
            ArrayPool<byte>.Shared.Return(_orderedNeurons, true);
        }
    }
    
    private static List<Gene>[] InitializeGraph(Gene[] genes)
    {
        List<Gene>[] graph = ArrayPool<List<Gene>>.Shared.Rent(byte.MaxValue);

        foreach (var gene in genes)
        {
            var outgoing = graph[gene.Source] ??= [];
            outgoing.Add(gene);
        }

        return graph;
    }

    private static byte[] InitializeOrderedNeurons(Gene[] genes, List<Gene>[] graph, out int neuronsCount)
    {
        Span<byte> orderedNeurons = stackalloc byte[genes.Length * 2];
        
        neuronsCount = TopologicalSorting.Sort(genes, graph, orderedNeurons);
        
        var resultArray = ArrayPool<byte>.Shared.Rent(neuronsCount);
        orderedNeurons.CopyTo(resultArray);
        return resultArray;
    }
}
