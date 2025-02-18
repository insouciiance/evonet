using System.Runtime.InteropServices;

namespace EvoNet.Core;

[StructLayout(LayoutKind.Sequential)]
public struct Gene
{
    private const float WEIGHT_COEFFICIENT = short.MaxValue / 5f;
    
    public byte Source;
    
    public byte Target;

    public short ShortWeight
    {
        get => (short)(Weight * WEIGHT_COEFFICIENT);
        set => Weight = value / WEIGHT_COEFFICIENT;
    }

    public float Weight;
}
