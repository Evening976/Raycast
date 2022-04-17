using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Raycast.DesktopClient
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Wall> walls;
        Texture2D _wallTex, _rayHitTex, _rayTex;

        List<Vector2> UniquePoints;
        List<float> UniqueAngles;

        //Ray ray;
        List<Ray> rays;
        Vector2 mouse;
        const float raycount = 20;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            float Width = GraphicsDevice.Viewport.Width;
            float Height = GraphicsDevice.Viewport.Height;

            walls = new List<Wall>{
                //border
                new Wall(0, 0, Width, 0),
                new Wall(Width, 0, Width, Height),
                new Wall(0, Height, Width, Height),
                new Wall(0,0,0,Height),
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

            _wallTex = new Texture2D(GraphicsDevice, 1, 1);
            _wallTex.SetData<Color>(new Color[] { Color.Black });

            _rayHitTex = Content.Load<Texture2D>("circle-32");

            _rayTex = new Texture2D(GraphicsDevice, 1, 1);
            _rayTex.SetData<Color>(new Color[] { Color.Red });

            //ray = new Ray(new Vector2(50, 150), new Vector2(1, 0));

            rays = new List<Ray>();

            UniquePoints = new List<Vector2>();
            UniqueAngles = new List<float>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

             mouse = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            rays.Clear();
            for (float i = 0; i < 360; i+= 360/raycount)
            {
                rays.Add(new Ray(mouse, new Vector2((float)Math.Cos((i*Math.PI)/180), (float)Math.Sin((i*Math.PI)/180))));
            }

            UniquePoints.Clear();

            for (int r = 0; r < rays.Count; r++)
            {
                Vector2 closest = new Vector2();
                float record = float.MaxValue;

                for (int w = 0; w < walls.Count; w++)
                {
                    Vector2 hit = rays[r].Cast(walls[w].start, walls[w].end);

                    if (hit != new Vector2(32383773.23f, 32383773.23f))
                    {
                        float d = Vector2.Distance(mouse, hit);
                        if (d < record)
                        {
                            record = Vector2.Distance(mouse, hit);
                            closest = hit;
                        }
                    }
                }

                UniquePoints.Add(closest);
            }

            for (int a = 0; a < UniquePoints.Count; a++)
            {
                float angle = (float)Math.Atan2(UniquePoints[a].Y, UniquePoints[a].X);
                UniqueAngles.Add(angle - (1/100000));
                UniqueAngles.Add(angle);
                UniqueAngles.Add(angle + (1/100000));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //ray.Draw(_spriteBatch, _rayTex);

            foreach (Wall w in walls)
            {
                w.Draw(_spriteBatch, _wallTex);
            }

            foreach(Ray r in rays){
                r.Draw(_spriteBatch,  _rayTex);
            }

            foreach (Vector2 rH in UniquePoints)
            {
                _spriteBatch.Draw(_rayHitTex, new Rectangle((int)rH.X - 8, (int)rH.Y - 8, 16, 16), Color.White);
                DrawLine(_spriteBatch, _rayTex, mouse, rH);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawLine(SpriteBatch _sb, Texture2D _tex, Vector2 start, Vector2 end){
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            _sb.Draw(_tex,
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
