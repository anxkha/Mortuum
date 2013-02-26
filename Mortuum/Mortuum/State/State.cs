using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.State
{
    internal abstract class State
    {
        protected Player player;
        protected bool firstFrame;
        protected ContentManager content;
        protected GraphicsDeviceManager graphics;

        public abstract bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player);

        public abstract void Unload();

        public abstract GameState Update(float elapsedGameTime);

        public abstract void Draw();
    }
}
