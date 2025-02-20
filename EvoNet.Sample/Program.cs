using EvoNet.Generation;
using EvoNet.Sample;
using EvoNet.Selection;
using EvoNet.Selection.Crossovers;
using EvoNet.Selection.Mutations;
using EvoNet.Selection.Selectors;
using EvoNet.Simulation;

DefaultWorldGenerator worldGenerator = new()
{
    PopulationSize = 1000,
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
    GenerationCount = 100,
    StepsPerGeneration = 100
};

SimulationRunner runner = new()
{
    World = worldGenerator.Generate(),
    NaturalSelection = selection,
    Configuration = configuration
};

runner.StepFinished += (world, genIndex, stepIndex) =>
{
    Directory.CreateDirectory($"Gen_{genIndex}");
    BitmapHelper.DumpToBitmap(world, $"Gen_{genIndex}/Step_{stepIndex}_{world.Agents.Length}Agents.png");
};

runner.GenerationFinished += (world, i) =>
{
    Console.WriteLine(i);
};


runner.Run();
