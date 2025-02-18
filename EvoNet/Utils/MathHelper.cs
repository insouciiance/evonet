namespace EvoNet.Utils;

public static class MathHelper
{
    public static float Tanh(float x)
    {
        var t = x * (27 + x * x) / (27 + 9 * x * x);
        t = Math.Clamp(t, -1, 1);
        return t;
    }
}
