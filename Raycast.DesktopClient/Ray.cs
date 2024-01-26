using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Raycast.DesktopClient
{
    public class Ray
    {
        Vector2 Position, Direction;
        public Ray(Vector2 _pos, Vector2 _direction)
        {
            Position = _pos;
            Direction = _direction;
        }

        public void LookAt(Vector2 dir)
        {
            Direction.X = dir.X - Position.X;
            Direction.Y = dir.Y - Position.Y;
            Direction = Vector2.Normalize(Direction);
        }
        public Vector2 Cast(Vector2 _objStart, Vector2 _objEnd)
        {
            float x1 = _objStart.X;
            float y1 = _objStart.Y;
            float x2 = _objEnd.X;
            float y2 = _objEnd.Y;

            float x3 = Position.X;
            float y3 = Position.Y;
            float x4 = Position.X + Direction.X;
            float y4 = Position.Y + Direction.Y;


            float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (den == 0)
            {
                return new Vector2(32383773.23f, 32383773.23f);
            }

            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;

            if (t > 0 && t < 1 && u > 0)
            {
                Vector2 pt = new Vector2();
                pt.X = x1 + (t * (x2 - x1));
                pt.Y = y1 + (t * (y2 - y1));

                Vector2 pu = new Vector2();
                pu.X = x3 + u * (x4 - x3);
                pu.Y = y3 + u * (y4 - y3);
                return pu;
            }
            else { return new Vector2(32383773.23f, 32383773.23f); }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tex) => spriteBatch.Draw(tex, new Rectangle((int)Position.X, (int)Position.Y, (int)Direction.Length() * 10, 2),
                null, Color.White, (float)Math.Atan2(Direction.Y, Direction.X), Vector2.Zero, SpriteEffects.None, 0);


    }
}
