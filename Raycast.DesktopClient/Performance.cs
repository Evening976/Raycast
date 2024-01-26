using Microsoft.Xna.Framework;
using System;

namespace Raycast.DesktopClient
{
    internal class Performance
    {
        private int frames;
        private double elapsedTime;
        private int fps;

        public Performance()
        {
            frames = 0;
            elapsedTime = 0;
            fps = 0;
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            frames++;
            if (elapsedTime >= 500)
            {
                fps = frames * 2;
                frames = 0;
                elapsedTime = 0;
            }

            Console.Write(fps + "FPS\n");
        }
    }
}
