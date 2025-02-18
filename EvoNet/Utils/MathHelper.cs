using System.Runtime.CompilerServices;

namespace EvoNet.Utils;

public static class MathHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Tanh(float x)
    {
        return x * (27 + x * x) / (27 + 9 * x * x);
    }
}
