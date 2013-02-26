using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Enemy
{
    internal class Soul : EnemyBase
    {
        public const int MAGIC = 13;

        public Soul(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
        }
    }
}
