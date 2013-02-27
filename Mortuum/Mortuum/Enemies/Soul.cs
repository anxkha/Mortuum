using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Enemies
{
    internal class Soul : Enemy
    {
        public const int MAGIC = 13;

        public Soul(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
        }
    }
}
