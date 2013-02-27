using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mortuum.Common;
using Mortuum.Enemies;
using Mortuum.Levels;
using Mortuum.Players;
using Mortuum.Ui;
using Mortuum.Weapons;

namespace Mortuum.States
{
    internal class TitleScreen : State
    {
        private Level _level;
        private Weapon _weapon;
        private Enemy _archer;
        private Enemy _guard;

        private TextBox _txtbox;
        private Canvas _menuCanvas;

        public TitleScreen(ContentManager content, GraphicsDeviceManager graphics, Player player)
            : base(content, graphics, player)
        {
        }

        public override void Load()
        {
            _level = LevelFactory.Create(1);

            _weapon = WeaponFactory.Create(WeaponType.Sword);
            _weapon.Position = new Vector3(0.0f, 0.5f, 0.0f);
            _weapon.Direction = 0.0f;

            var canvasWidth = 400;
            var canvasHeight = 500;
            _menuCanvas = new Canvas()
            {
                IsActive = true,
                Size = new Vector2(canvasWidth, canvasHeight),
                Position = new Vector2(((_graphics.GraphicsDevice.Viewport.Width / 2) - (canvasWidth / 2)), ((_graphics.GraphicsDevice.Viewport.Height / 2) - (canvasHeight / 2))),
                Hidden = true
            };
            _menuCanvas.Load(_content, _graphics);

            _txtbox = new TextBox
            {
                Font = "Verdana",
                Text = "Hi!",
                Name = "txtHi"
            };
            _txtbox.Load(_content, _graphics);

            _menuCanvas.AddChild(_txtbox);

            _archer = EnemyFactory.Create(EnemyType.Archer, 1);
            _archer.SetPosition(new Vector3(1.0f, 0.3f, 1.0f));

            _guard = EnemyFactory.Create(EnemyType.Guard, 1);
            _guard.SetPosition(new Vector3(-1.0f, 0.3f, 1.0f));

            _player.Position = new Vector3(0.0f, 0.35f, 0.0f);
        }

        public override void Unload()
        {
            _weapon.Unload();
            _level.Unload();

            _weapon = null;
            _level = null;
        }

        public override GameState Update(float elapsedTime)
        {
            if (_firstFrame)
            {
                _firstFrame = false;
                return GameState.TitleScreen;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                return GameState.Exit;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                return GameState.Exit;

            Camera.LookAt(new Vector3(0.0f, 10.0f, -0.1f), new Vector3(0.0f, 0.0f, 0.0f));

            // Handle player input.
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _player.Direction = _player.Direction + Settings.PlayerTurnSpeed * elapsedTime;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _player.Direction = _player.Direction - Settings.PlayerTurnSpeed * elapsedTime;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _player.Move(true, elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _player.Move(false, elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                _player.WeaponPosition = _player.WeaponPosition + (Settings.PlayerWeaponSwingSpeed * elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.E))
                _player.WeaponPosition = _player.WeaponPosition - (Settings.PlayerWeaponSwingSpeed * elapsedTime);

            // Adjust weapon position and direction.
            if (_player.WeaponPosition < -90.0f) _player.WeaponPosition = -90.0f;
            if (_player.WeaponPosition > 90.0f) _player.WeaponPosition = 90.0f;

            _weapon.Direction =_player.Direction + _player.WeaponPosition;
            _weapon.Position = _player.Position;

            // Update all the entities.
            _weapon.Update(elapsedTime);
            _archer.Update(elapsedTime);
            _guard.Update(elapsedTime);
            _player.Update(elapsedTime);
            _menuCanvas.Update(elapsedTime);

            return GameState.TitleScreen;
        }

        public override void Draw()
        {
            var state = new DepthStencilState();

            state.DepthBufferEnable = true;

            _graphics.GraphicsDevice.DepthStencilState = state;
            _graphics.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            _level.Draw(Camera.View, Camera.Projection);
            _weapon.Draw(Camera.View, Camera.Projection);
            _archer.Draw(Camera.View, Camera.Projection);
            _guard.Draw(Camera.View, Camera.Projection);
            _player.Draw(Camera.View, Camera.Projection);

            _menuCanvas.Draw();
        }
    }
}
