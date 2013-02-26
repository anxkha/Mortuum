using System;

namespace Mortuum.Weapon
{
    internal class InvalidWeaponException : Exception
    {
        public InvalidWeaponException(WeaponType weaponType)
            : base("Invalid weapon type: " + weaponType.ToString())
        {
        }
    }
}
