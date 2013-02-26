using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Enemy
{
    internal class Archer : EnemyBase
    {
        public Archer(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
        }

        public override void Load()
        {
            Model = Content.Load<Model>("archer");
            base.Load();
        }
    }
}
