using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raycast.DesktopClient
{
    public class Wall
    {
        public Vector2 start, end;
        public Wall(float startX, float startY, float endX, float endY)
        {
            start = new Vector2(startX, startY);
            end = new Vector2(endX, endY);
        }

        public void Draw(SpriteBatch s, Texture2D tex) => Utils.drawLine(s, tex, start, end, Color.Black);

    }
}
