using System.Runtime.InteropServices;

namespace EvoNet.Core;

[StructLayout(LayoutKind.Sequential)]
public struct Gene
{
    private const float WEIGHT_COEFFICIENT = short.MaxValue / 5f;
    
    public byte Source;
    
    public byte Target;
    
    public short Weight;

    public float GetWeight() => Weight / WEIGHT_COEFFICIENT;
}
