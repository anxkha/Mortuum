using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Enemy
{
    internal class Wizard : EnemyBase
    {
        public Wizard(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
        }

        public override void Load()
        {
            Model = Content.Load<Model>("wizard");
            base.Load();
        }
    }
}
