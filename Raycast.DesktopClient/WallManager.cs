using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Raycast.DesktopClient
{
    internal class WallManager
    {
        List<Wall> walls;
        Texture2D _wallTex;

        public WallManager(GraphicsDevice graphicsDevice)
        {
            walls = new List<Wall>{
                //border
                new(0, 0, Constants.Width, 0),
                new(Constants.Width, 0, Constants.Width, Constants.Height),
                new(0, Constants.Height, Constants.Width, Constants.Height),
                new(0,0,0,Constants.Height),
                //P1
                new(100, 150, 120, 50),
                new(120, 50, 200, 80),
                new(200, 80, 140, 210),
                new(140, 210, 100, 150),
                //P2
                new(100, 200, 120, 250),
                new(120, 250, 60, 300),
                new(60, 300, 100, 200),
                //P3
                new(200, 260, 220, 150),
                new(220, 150, 300, 200),
                new(300, 200, 350, 320),
                new(350, 320, 200, 260),
                //P4
                new(340, 60, 360, 40),
                new(360, 40, 370, 70),
                new(370, 70, 340, 60),
                //P5
                new(450, 190, 560, 170),
                new(560, 170, 540, 270),
                new(540, 270, 430, 290),
                new(430, 290, 450, 190),
                //P6
                new(400, 95, 580, 50),
                new(580, 50, 480, 150),
                new(480, 150, 400, 95)
            };


            _wallTex = new Texture2D(graphicsDevice, 1, 1);
            _wallTex.SetData(new Color[] { Color.Black });
        }

        public List<Wall> getWalls() => walls;

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Wall wall in walls)
            {
                wall.Draw(spriteBatch, _wallTex);
            }
        }
    }
}
