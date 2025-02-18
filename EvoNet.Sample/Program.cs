using EvoNet.Generation;
using EvoNet.Selection;
using EvoNet.Selection.Crossovers;
using EvoNet.Selection.Mutations;
using EvoNet.Selection.Selectors;
using EvoNet.Simulation;

DefaultWorldGenerator worldGenerator = new()
{
    PopulationSize = 300,
    GeneCount = 8
};

NaturalSelection selection = new()
{
    Selector = new DefaultSelector(),
    Crossover = new DefaultCrossover(),
    Mutation = new DefaultMutation()
};

SimulationConfig configuration = new()
{
    GenerationCount = 1000,
    StepsPerGeneration = 100
};

SimulationRunner runner = new()
{
    World = worldGenerator.Generate(),
    NaturalSelection = selection,
    Configuration = configuration
};

runner.Run();
