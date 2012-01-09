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
    class GameScreen : State
    {
        private int currentLevel;

        public override bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player)
        {
            return Load(content, graphics, player, 0);
        }

        public bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player, int level)
        {
            if ((level < Settings.MinLevels) || (level > Settings.MaxLevels))
                return false;

            currentLevel = level;
            firstFrame = true;

            this.player = player;
            this.content = content;
            this.graphics = graphics;

            return true;
        }

        public override void Unload()
        {
        }

        public override GameState Update(float elapsedTime)
        {
            if (firstFrame)
            {
                firstFrame = false;
                return GameState.GameScreen;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                return GameState.TitleScreen;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                return GameState.TitleScreen;

            return GameState.GameScreen;
        }

        public override void Draw()
        {
        }
    }
}
