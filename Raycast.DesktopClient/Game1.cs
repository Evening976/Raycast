using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Raycast.DesktopClient
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private WallManager _wallManager;
        private RayManager _raysManager;

        private Performance _fpsCounter;

        private Vector2 _mouse;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Constants.Width;
            _graphics.PreferredBackBufferHeight = Constants.Height;
            _graphics.SynchronizeWithVerticalRetrace = Constants.vSync;
            IsFixedTimeStep = Constants.vSync;
            _graphics.ApplyChanges();

            _wallManager = new WallManager(GraphicsDevice);
            _raysManager = new RayManager(GraphicsDevice, Content, Constants.RayCount);

            _fpsCounter = new Performance();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mouse = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            _raysManager.Update(_mouse, _wallManager.getWalls());

            _fpsCounter.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin();

            _wallManager.Draw(_spriteBatch);
            _raysManager.Draw(_spriteBatch, _mouse);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
