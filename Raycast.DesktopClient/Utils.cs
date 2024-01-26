using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Raycast.DesktopClient
{
    public static class Utils
    {
        public static void drawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, Color color,
            float thickness = 1f)
        {
            if (start == end) return;

            float distance = Vector2.Distance(start, end);
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            spriteBatch.Draw(texture, start, null, color, angle, Vector2.Zero, new Vector2(distance, thickness), SpriteEffects.None, 0);
        }
    }
}
