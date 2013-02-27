using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Mortuum.Players;

namespace Mortuum.States
{
    internal abstract class State
    {
        protected Player _player;
        protected bool _firstFrame;
        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;

        public int CurrentLevel
        {
            get;
            set;
        }

        public State(ContentManager content, GraphicsDeviceManager graphics, Player player)
        {
            _content = content;
            _graphics = graphics;
            _player = player;
            _firstFrame = true;
        }

        public abstract void Load();

        public abstract void Unload();

        public abstract GameState Update(float elapsedGameTime);

        public abstract void Draw();
    }
}
