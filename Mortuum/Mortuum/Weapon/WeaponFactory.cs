using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Weapon
{
    internal class WeaponFactory
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }

        public static IWeapon Create(WeaponType weaponType)
        {
            IWeapon newWeapon;

            switch (weaponType)
            {
                case WeaponType.Sword:
                    newWeapon = new Sword(_content, _graphics);
                    break;
                case WeaponType.Mace:
                    newWeapon = new Mace(_content, _graphics);
                    break;
                case WeaponType.Axe:
                    newWeapon = new Axe(_content, _graphics);
                    break;
                case WeaponType.Hammer:
                    newWeapon = new Hammer(_content, _graphics);
                    break;
                default:
                    throw new InvalidWeaponException(weaponType);
            }

            newWeapon.Load();
            return newWeapon;
        }
    }
}
