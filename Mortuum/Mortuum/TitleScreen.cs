﻿using System;
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
        public override bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player)
        {
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
                return GameState.TitleScreen;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                return GameState.Exit;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                return GameState.Exit;

            return GameState.TitleScreen;
        }

        public override void Draw()
        {
        }
    }
}
