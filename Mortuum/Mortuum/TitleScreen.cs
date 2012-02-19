using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mortuum.Ui;

namespace Mortuum
{
    class TitleScreen : State
    {
        Level level;
        Weapon weapon;
        Archer archer;
        Guard guard;

        TextBox txtbox;
        Canvas menuCanvas;

        public override bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player)
        {
            firstFrame = true;

            this.player = player;
            this.content = content;
            this.graphics = graphics;

            level = new Level();
            level.Load(1, content, graphics);

            weapon = new Weapon() { Type = WeaponType.Sword, Position = new Vector3(0.0f, 0.5f, 0.0f), Direction = 0.0f };
            weapon.Load(content, graphics);

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

            archer = new Archer() { Position = new Vector3(1.0f, 0.3f, 1.0f) };
            archer.Load(content, graphics);

            guard = new Guard() { Position = new Vector3(-1.0f, 0.3f, 1.0f) };
            guard.Load(content, graphics);

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

            Camera.LookAt(new Vector3(0.0f, 5.0f, -0.1f), new Vector3(0.0f, 0.0f, 0.0f));

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                var tempDir = player.Direction;

                tempDir.Z += Settings.PlayerTurnSpeed * elapsedTime;

                player.Direction = tempDir;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                var tempDir = player.Direction;

                tempDir.Z -= Settings.PlayerTurnSpeed * elapsedTime;

                player.Direction = tempDir;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                player.WeaponPosition = player.WeaponPosition + (Settings.PlayerWeaponSwingSpeed * elapsedTime);

            if (Keyboard.GetState().IsKeyDown(Keys.E))
                player.WeaponPosition = player.WeaponPosition - (Settings.PlayerWeaponSwingSpeed * elapsedTime);

            if (player.WeaponPosition < -90.0f) player.WeaponPosition = -90.0f;
            if (player.WeaponPosition > 90.0f) player.WeaponPosition = 90.0f;

            weapon.Direction = -player.Direction.Z - player.WeaponPosition;

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
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            level.Draw(Camera.View, Camera.Projection);
            weapon.Draw(Camera.View, Camera.Projection);
            archer.Draw(Camera.View, Camera.Projection);
            guard.Draw(Camera.View, Camera.Projection);
            player.Draw(Camera.View, Camera.Projection);

            menuCanvas.Draw();
        }
    }
}
