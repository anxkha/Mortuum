using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Enemies
{
    internal class Archer : Enemy
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
