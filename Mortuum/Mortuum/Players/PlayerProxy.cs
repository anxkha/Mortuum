using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Players
{
    internal class PlayerProxy
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;
        private static Player _player;

        public static Player Current
        {
            get
            {
                if (null == _player)
                    CreatePlayer();
                return _player;
            }
        }

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }

        private static void CreatePlayer()
        {
            _player = new Player(_content, _graphics);
            _player.Load();
        }
    }
}
