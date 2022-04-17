using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Raycast.DesktopClient
{
    public class Wall
    {
        public Vector2 start, end;
        public Wall(float x1, float y1, float x2, float y2)
        {
            start = new Vector2(x1, y1);
            end = new Vector2(x2, y2);
        }
        public void Draw(SpriteBatch sb, Texture2D t)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            sb.Draw(t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    2), //width of line, change this to make thicker line
                null,
                Color.Black, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                1);
        }
    }
}
