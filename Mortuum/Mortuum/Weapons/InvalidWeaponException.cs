using System;

namespace Mortuum.Weapons
{
    internal class InvalidWeaponException : Exception
    {
        public InvalidWeaponException(WeaponType weaponType)
            : base("Invalid weapon type: " + weaponType.ToString())
        {
        }
    }
}
