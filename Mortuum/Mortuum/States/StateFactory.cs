using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Mortuum.Players;
using Mortuum.States;

namespace Mortuum.States
{
    internal class StateFactory
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;
        private static Player _player;

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
            _player = PlayerProxy.Current;
        }

        public static State Create(GameState gameState, int level = 0)
        {
            State newState;

            switch (gameState)
            {
                case GameState.TitleScreen:
                    newState = new TitleScreen(_content, _graphics, _player);
                    break;
                case GameState.GameScreen:
                    newState = new GameScreen(_content, _graphics, _player) { CurrentLevel = level };
                    break;
                default:
                    throw new InvalidStateException(gameState);
            }

            newState.Load();
            return newState;
        }
    }
}
