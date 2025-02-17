using System.Runtime.InteropServices;

namespace EvoNet.Core;

[StructLayout(LayoutKind.Sequential)]
public struct Gene
{
    public byte Source;
    
    public byte Target;
    
    public short Weight;
}
