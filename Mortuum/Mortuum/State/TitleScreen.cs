using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mortuum.Common;
using Mortuum.Enemy;
using Mortuum.Ui;
using Mortuum.Weapon;

namespace Mortuum.State
{
    internal class TitleScreen : State
    {
        private Level level;
        private IWeapon weapon;
        private IEnemy archer;
        private IEnemy guard;

        private TextBox txtbox;
        private Canvas menuCanvas;

        public override bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player)
        {
            firstFrame = true;

            this.player = player;
            this.content = content;
            this.graphics = graphics;

            level = new Level();
            level.Load(1, content, graphics);

            weapon = WeaponFactory.Create(WeaponType.Sword);
            weapon.SetPosition(new Vector3(0.0f, 0.5f, 0.0f));
            weapon.SetDirection(0.0f);

            menuCanvas = new Canvas();
            menuCanvas.IsActive = true;
            menuCanvas.Size = new Vector2(400, 500);
            menuCanvas.Position = new Vector2(((graphics.GraphicsDevice.Viewport.Width / 2) - (menuCanvas.Size.X / 2)), ((graphics.GraphicsDevice.Viewport.Height / 2) - (menuCanvas.Size.Y / 2)));
            menuCanvas.Hidden = true;
            menuCanvas.Load(content, graphics);

            txtbox = new TextBox();
            txtbox.Font = "Verdana";
            txtbox.Text = "Hi!";
            txtbox.Name = "txtHi";
            txtbox.Load(content, graphics);

            menuCanvas.AddChild(txtbox);

            archer = EnemyFactory.Create(EnemyType.Archer, 1);
            archer.SetPosition(new Vector3(1.0f, 0.3f, 1.0f));

            guard = EnemyFactory.Create(EnemyType.Guard, 1);
            guard.SetPosition(new Vector3(-1.0f, 0.3f, 1.0f));

            player.Position = new Vector3(0.0f, 0.35f, 0.0f);

            return true;
        }

        public override void Unload()
        {
            weapon.Unload();
            level.Unload();

            weapon = null;
            level = null;
        }

        public override GameState Update(float elapsedTime)
        {
            if (firstFrame)
            {
                firstFrame = false;
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
                player.Direction = player.Direction + Settings.PlayerTurnSpeed * elapsedTime;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                player.Direction = player.Direction - Settings.PlayerTurnSpeed * elapsedTime;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                player.Move(true, elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                player.Move(false, elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                player.WeaponPosition = player.WeaponPosition + (Settings.PlayerWeaponSwingSpeed * elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.E))
                player.WeaponPosition = player.WeaponPosition - (Settings.PlayerWeaponSwingSpeed * elapsedTime);

            // Adjust weapon position and direction.
            if (player.WeaponPosition < -90.0f) player.WeaponPosition = -90.0f;
            if (player.WeaponPosition > 90.0f) player.WeaponPosition = 90.0f;

            weapon.SetDirection(player.Direction + player.WeaponPosition);
            weapon.SetPosition(player.Position);

            // Update all the entities.
            weapon.Update(elapsedTime);
            archer.Update(elapsedTime);
            guard.Update(elapsedTime);
            player.Update(elapsedTime);
            menuCanvas.Update(elapsedTime);

            return GameState.TitleScreen;
        }

        public override void Draw()
        {
            var state = new DepthStencilState();

            state.DepthBufferEnable = true;

            graphics.GraphicsDevice.DepthStencilState = state;
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            level.Draw(Camera.View, Camera.Projection);
            weapon.Draw(Camera.View, Camera.Projection);
            archer.Draw(Camera.View, Camera.Projection);
            guard.Draw(Camera.View, Camera.Projection);
            player.Draw(Camera.View, Camera.Projection);

            menuCanvas.Draw();
        }
    }
}
