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

            weapon = new Weapon() { Type = WeaponType.Sword, Position = new Vector3(0.0f, 4.0f, 1.0f) };
            weapon.Load(content, graphics);

            menuCanvas = new Canvas();

            menuCanvas.IsActive = true;
            menuCanvas.Position = new Vector2(((graphics.GraphicsDevice.Viewport.Width / 2) - 200), ((graphics.GraphicsDevice.Viewport.Height / 2) - 250));
            menuCanvas.Size = new Vector2(400, 500);
            menuCanvas.Hidden = true;
            menuCanvas.Load(content, graphics);

            txtbox = new TextBox();
            txtbox.Font = "Verdana";
            txtbox.Text = "Hi!";
            txtbox.Name = "txtHi";
            txtbox.Load(content, graphics);

            menuCanvas.AddChild(txtbox);

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

            weapon.Update(elapsedTime);

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

            menuCanvas.Draw();
        }
    }
}
