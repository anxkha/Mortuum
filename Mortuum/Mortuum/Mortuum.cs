using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mortuum.Common;
using Mortuum.Enemies;
using Mortuum.Levels;
using Mortuum.Players;
using Mortuum.Portals;
using Mortuum.Spells;
using Mortuum.States;
using Mortuum.Weapons;

namespace Mortuum
{
    internal class Mortuum : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;
        private State _gameScreen;
        private State _titleScreen;

        private GameState _currentGameState;
        private GameState _lastGameState;
        private GameState _nextGameState;

        private float _fpsTime;
        private int _fps;

        public Mortuum()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <remarks>
        /// Load non-graphics content here.
        /// Calling base.Initialize will enumerate through any components and initialize them as well.
        /// </remarks>
        protected override void Initialize()
        {
            InitializeWindow();
            InitializeGraphics();
            InitializeCamera();
            InitializeProxies();
            InitializeFactories();
            
            Debug.Start("debug.log");
            Debug.Write("Mortuum starting.");

            _fps = 0;
            _fpsTime = 0;

            base.Initialize();
        }

        private void InitializeWindow()
        {
            Window.Title = Settings.GameTitle;
            IsFixedTimeStep = Settings.FixedTimeStep;
        }

        private void InitializeGraphics()
        {
            _graphics.PreferredBackBufferWidth = Settings.GraphicsWidth;
            _graphics.PreferredBackBufferHeight = Settings.GraphicsHeight;
            _graphics.PreferredBackBufferFormat = Settings.GraphicsFormat;
            _graphics.IsFullScreen = Settings.GraphicsFullScreen;
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24;
            _graphics.SynchronizeWithVerticalRetrace = Settings.SyncWithVTrace;
            _graphics.ApplyChanges();
        }

        private void InitializeCamera()
        {
            Camera.Resize(45.0f, _graphics.GraphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f);
        }

        private void InitializeProxies()
        {
            PlayerProxy.Initialize(Content, _graphics);
        }

        private void InitializeFactories()
        {
            WeaponFactory.Initialize(Content, _graphics);
            EnemyFactory.Initialize(Content, _graphics);
            StateFactory.Initialize(Content, _graphics);
            LevelFactory.Initialize(Content, _graphics);
            PortalFactory.Initialize(Content, _graphics);
            SpellFactory.Initialize(Content, _graphics);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _titleScreen = StateFactory.Create(GameState.TitleScreen);
            _gameScreen = StateFactory.Create(GameState.GameScreen, 1);

            _currentGameState = GameState.TitleScreen;
            _lastGameState = GameState.TitleScreen;
            _nextGameState = GameState.TitleScreen;
        }

        protected override void UnloadContent()
        {
            _titleScreen.Unload();
            _gameScreen.Unload();

            Debug.Write("Mortuum stopping.");
            Debug.Stop();
        }

        protected override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            if (_nextGameState != _currentGameState)
            {
                if (GameState.Exit == _nextGameState)
                    this.Exit();

                _lastGameState = _currentGameState;
                _currentGameState = _nextGameState;
            }
            
            switch(_currentGameState)
            {
                case GameState.TitleScreen:
                    _nextGameState = _titleScreen.Update(elapsedTime);
                    break;

                case GameState.GameScreen:
                    _nextGameState = _gameScreen.Update(elapsedTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float elapsedTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

            _fps = (int)(1.0f / elapsedTime);

            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            if (_fpsTime > 1.0f)
            {
                _fpsTime -= 1.0f;
                _fps = 0;
            }

            switch (_currentGameState)
            {
                case GameState.TitleScreen:
                    _titleScreen.Draw();
                    break;

                case GameState.GameScreen:
                    _gameScreen.Draw();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
