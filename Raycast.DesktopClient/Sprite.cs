using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raycast.DesktopClient
{
    public class Sprite
    {

        Vector2 Position;

        float _speed = 0.2f;
        Vector2 Direction;
        Texture2D _texture;
        public Sprite(int x, int y, Texture2D _tex)
        {
            Position = new Vector2(x, y);
            _texture = _tex;
            Direction = new Vector2((float)Math.Cos(Math.PI / 4), (float)Math.Sin(Math.PI / 4));
        }

        public void Update(GameTime elapsedGameTime)
        {
            Position += Direction * _speed;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, Position, null, Color.White);
        }
    }
}
