using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum
{
    enum GameState
    {
        TitleScreen = 1,
        WinScreen,
        LoseScreen,
        GameScreen,
        Exit
    };

    class State
    {
        protected Player player;

        protected bool firstFrame;

        protected ContentManager content;
        protected GraphicsDeviceManager graphics;

        public virtual bool Load(ContentManager content, GraphicsDeviceManager graphics, Player player)
        {
            return false;
        }

        public virtual void Unload()
        {
        }

        public virtual GameState Update(float elapsedGameTime)
        {
            return GameState.TitleScreen;
        }

        public virtual void Draw()
        {
        }
    }
}
