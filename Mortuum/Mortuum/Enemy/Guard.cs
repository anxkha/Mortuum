using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Enemy
{
    internal class Guard : EnemyBase
    {
        public Guard(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
        }

        public override void Load()
        {
            Model = Content.Load<Model>("guard");
            base.Load();
        }
    }
}
