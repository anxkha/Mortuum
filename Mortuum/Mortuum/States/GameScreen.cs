using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Mortuum.Common;
using Mortuum.Players;

namespace Mortuum.States
{
    internal class GameScreen : State
    {
        public GameScreen(ContentManager content, GraphicsDeviceManager graphics, Player player)
            : base(content, graphics, player)
        {
        }

        public override void Load()
        {
            if ((CurrentLevel < Settings.MinLevels) || (CurrentLevel > Settings.MaxLevels))
                throw new InvalidLevelException(CurrentLevel);
        }

        public override void Unload()
        {
        }

        public override GameState Update(float elapsedTime)
        {
            if (_firstFrame)
            {
                _firstFrame = false;
                return GameState.GameScreen;
            }

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
