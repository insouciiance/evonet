using EvoNet.Core;
using EvoNet.Selection.Crossovers;
using EvoNet.Selection.Mutations;
using EvoNet.Selection.Selectors;

namespace EvoNet.Selection;

public class NaturalSelection
{
    public ISelector Selector { get; init; }
    
    public ICrossover Crossover { get; init; }
    
    public IMutation Mutation { get; init; }

    public void RunOnce(World world)
    {
        var selectedAgents = SelectAgents(world);

        var children = CrossoverAgents(world, selectedAgents);
        
        MutateAgents(world, children);
        
        world.SetAgents(children.ToArray());
    }

    private List<Agent> SelectAgents(World world)
    {
        List<Agent> selectedAgents = [];

        foreach (var agent in world.Agents)
        {
            if (Selector.Select(agent, world))
            {
                selectedAgents.Add(agent);
            }
        }

        return selectedAgents;
    }

    private List<Agent> CrossoverAgents(World world, List<Agent> selectedAgents)
    {
        List<Agent> children = [];

        for (int i = 0; i < world.Agents.Length; i++)
        {
            int parent1Index = Random.Shared.Next(0, selectedAgents.Count);
            int parent2Index = Random.Shared.Next(0, selectedAgents.Count);
            
            var parent1 = selectedAgents[parent1Index];
            var parent2 = selectedAgents[parent2Index];
            
            var child = Crossover.Produce(parent1, parent2);
            
            children.Add(child);
        }
        
        return children;
    }

    private void MutateAgents(World world, List<Agent> children)
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (Random.Shared.NextSingle() <= 0.001)
            {
                var child = children[i];
                child = Mutation.Mutate(child);
                children[i] = child;
            }
        }
    }
}
