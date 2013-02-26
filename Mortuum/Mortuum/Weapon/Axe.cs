using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Weapon
{
    internal class Axe : WeaponBase
    {
        public Axe(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
        }

        public override void Load()
        {
            model = _content.Load<Model>("axe");
            base.Load();
        }
    }
}
