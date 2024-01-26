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
                new Wall(0, 0, Constants.Width, 0),
                new Wall(Constants.Width, 0, Constants.Width, Constants.Height),
                new Wall(0, Constants.Height, Constants.Width, Constants.Height),
                new Wall(0,0,0,Constants.Height),
                //P1
                new Wall(100, 150, 120, 50),
                new Wall(120, 50, 200, 80),
                new Wall(200, 80, 140, 210),
                new Wall(140, 210, 100, 150),
                //P2
                new Wall(100, 200, 120, 250),
                new Wall(120, 250, 60, 300),
                new Wall(60, 300, 100, 200),
                //P3
                new Wall(200, 260, 220, 150),
                new Wall(220, 150, 300, 200),
                new Wall(300, 200, 350, 320),
                new Wall(350, 320, 200, 260),
                //P4
                new Wall(340, 60, 360, 40),
                new Wall(360, 40, 370, 70),
                new Wall(370, 70, 340, 60),
                //P5
                new Wall(450, 190, 560, 170),
                new Wall(560, 170, 540, 270),
                new Wall(540, 270, 430, 290),
                new Wall(430, 290, 450, 190),
                //P6
                new Wall(400, 95, 580, 50),
                new Wall(580, 50, 480, 150),
                new Wall(480, 150, 400, 95)
            };


            _wallTex = new Texture2D(graphicsDevice, 1, 1);
            _wallTex.SetData<Color>(new Color[] { Color.Black });
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
