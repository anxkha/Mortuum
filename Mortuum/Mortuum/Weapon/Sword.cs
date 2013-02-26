using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Weapon
{
    internal class Sword : WeaponBase
    {
        public Sword(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
        }

        public override void Load()
        {
            model = _content.Load<Model>("sword");
            base.Load();
        }
    }
}
