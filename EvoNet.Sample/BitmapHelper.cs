using System.Drawing;
using System.Runtime.Versioning;
using EvoNet.Core;

namespace EvoNet.Sample;

public static class BitmapHelper
{
    [SupportedOSPlatform(platformName: "windows")]
    public static void DumpToBitmap(World world, string fileName)
    {
        Bitmap bitmap = new(world.Width, world.Height);

        foreach (var agent in world.Agents)
        {
            var position = world.GetAgentPosition(agent);
            bitmap.SetPixel(position.X, position.Y, Color.Red);
        }
        
        bitmap.Save(fileName);
    }
}
