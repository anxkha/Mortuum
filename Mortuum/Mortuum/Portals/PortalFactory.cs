using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Portals
{
    internal class PortalFactory
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }

        public static Portal Create()
        {
            var newPortal = new Portal(_content, _graphics);
            newPortal.Load();
            return newPortal;
        }
    }
}
