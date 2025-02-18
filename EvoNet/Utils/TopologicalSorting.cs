using System.Runtime.InteropServices;
using EvoNet.Core;

namespace EvoNet.Utils;

public static class TopologicalSorting
{
    public static int Sort(Gene[] genes, Dictionary<byte, List<Gene>> graph, Span<byte> output)
    {
        Dictionary<byte, int> inDegree = [];

        foreach (var gene in genes)
        {
            inDegree[gene.Source] = 0;
            inDegree[gene.Target] = 0;
        }

        foreach (var gene in genes)
        {
            inDegree[gene.Target]++;
        }
        
        Queue<byte> queue = [];
        int outputIndex = 0;
        
        foreach (var (neuron, degree) in inDegree)
        {
            if (degree == 0)
            {
                queue.Enqueue(neuron);
            }
        }

        while (queue.Count > 0)
        {
            byte neuron = queue.Dequeue();
            
            output[outputIndex++] = neuron;

            if (!graph.TryGetValue(neuron, out var outgoing))
            {
                continue;
            }
            
            foreach (var gene in outgoing)
            {
                ref int degree = ref CollectionsMarshal.GetValueRefOrNullRef(inDegree, gene.Target);
                degree--;

                if (degree == 0)
                {
                    queue.Enqueue(gene.Target);
                }
            }
        }

        return outputIndex;
    }
}
