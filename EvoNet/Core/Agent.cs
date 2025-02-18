namespace EvoNet.Core;

public class Agent : IDisposable
{
    public Genome Genome;

    public void Dispose()
    {
        Genome.Dispose();
    }
}
