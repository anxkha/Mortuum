using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mortuum
{
    class TitleScreen : State
    {
        Level level;
        Weapon weapon;

        public override bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player)
        {
            firstFrame = true;

            this.player = player;
            this.content = content;
            this.graphics = graphics;

            level = new Level();
            level.Load(1, content, graphics);

            weapon = new Weapon() { Type = WeaponType.Sword, Position = new Vector3(0.0f, 4.0f, 1.0f) };
            weapon.Init(content, graphics);

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
        }
    }
}
