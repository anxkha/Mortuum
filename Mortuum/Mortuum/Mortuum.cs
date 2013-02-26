using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mortuum.Common;
using Mortuum.Enemy;
using Mortuum.State;
using Mortuum.Weapon;

namespace Mortuum
{
    internal class Mortuum : Game
    {
        private GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;

        Player player;
        GameScreen gameScreen;
        TitleScreen titleScreen;

        GameState currentGameState;
        GameState lastGameState;
        GameState nextGameState;

        float FPStime;
        int FPS;

        public Mortuum()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Settings.GraphicsWidth;
            _graphics.PreferredBackBufferHeight = Settings.GraphicsHeight;
            _graphics.PreferredBackBufferFormat = Settings.GraphicsFormat;
            _graphics.IsFullScreen = Settings.GraphicsFullScreen;
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24;
            _graphics.SynchronizeWithVerticalRetrace = Settings.SyncWithVTrace;
            _graphics.ApplyChanges();

            this.Window.Title = Settings.GameTitle;
            this.IsFixedTimeStep = Settings.FixedTimeStep;

            Camera.Resize(45.0f, _graphics.GraphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f);

            Debug.Start("debug.log");
            Debug.Write("Mortuum starting.");

            WeaponFactory.Initialize(Content, _graphics);
            EnemyFactory.Initialize(Content, _graphics);

            FPS = 0;
            FPStime = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player();
            player.Load(Content, _graphics);

            titleScreen = new TitleScreen();
            titleScreen.Load(Content, _graphics, player);

            gameScreen = new GameScreen();
            gameScreen.Load(Content, _graphics, player);

            currentGameState = GameState.TitleScreen;
            lastGameState = GameState.TitleScreen;
            nextGameState = GameState.TitleScreen;
        }

        protected override void UnloadContent()
        {
            titleScreen.Unload();

            Debug.Write("Mortuum stopping.");
            Debug.Stop();
        }

        protected override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            if (nextGameState != currentGameState)
            {
                if (GameState.Exit == nextGameState)
                    this.Exit();

                lastGameState = currentGameState;
                currentGameState = nextGameState;
            }
            
            switch(currentGameState)
            {
                case GameState.TitleScreen:
                    nextGameState = titleScreen.Update(elapsedTime);
                    break;

                case GameState.GameScreen:
                    nextGameState = gameScreen.Update(elapsedTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float elapsedTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            FPS = (int)(1.0f / elapsedTime);

            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            if (FPStime > 1.0f)
            {
                FPStime -= 1.0f;
                FPS = 0;
            }

            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    titleScreen.Draw();
                    break;

                case GameState.GameScreen:
                    gameScreen.Draw();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
