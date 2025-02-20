using System.Runtime.InteropServices;
using EvoNet.Core;

namespace EvoNet.Utils;

public static class TopologicalSorting
{
    public static int Sort(Gene[] genes, List<Gene>[] graph, Span<byte> output)
    {
        Span<int?> inDegree = stackalloc int?[byte.MaxValue];
        
        foreach (var gene in genes)
        {
            inDegree[gene.Source] ??= 0;
            inDegree[gene.Target] ??= 0;
            
            inDegree[gene.Target]++;
        }
        
        Queue<byte> queue = [];
        int outputIndex = 0;
        
        for (byte i = 0; i < inDegree.Length; i++)
        {
            int? degree = inDegree[i];
            
            if (degree == 0)
            {
                queue.Enqueue(i);
            }
        }

        while (queue.Count > 0)
        {
            byte neuron = queue.Dequeue();
            
            output[outputIndex++] = neuron;

            if (graph[neuron] is not { } outgoing)
            {
                continue;
            }
            
            foreach (var gene in outgoing)
            {
                inDegree[gene.Target]--;

                if (inDegree[gene.Target] == 0)
                {
                    queue.Enqueue(gene.Target);
                }
            }
        }

        return outputIndex;
    }
}
