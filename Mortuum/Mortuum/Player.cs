using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mortuum
{
    enum Spells
    {
        DragonBreath = 1,
        Lightning,
        Apocalypse,
        Gate
    }

    enum Weapons
    {
        Sword = 1,
        Mace,
        Axe,
        Hammer
    }

    class Player
    {
        private int health;
        private int maxHealth;
        private int strength;
        private int maxStrength;
        private int shield;
        private int maxShield;
        private int magic;
        private int maxMagic;

        private int points;

        private Spells activeSpell;
        private Weapons activeWeapon;

        private bool maceAvailable;
        private bool axeAvailable;

        private bool dying;     // Whether or not to play the dying animation.
        private int dyingTimer; // In seconds.

        public Player()
        {
            maxHealth = health = 10;
            maxStrength = strength = 10;
            maxShield = shield = 5;
            maxMagic = magic = 20;

            points = 0;

            activeSpell = Spells.DragonBreath;
            activeWeapon = Weapons.Sword;

            maceAvailable = false;
            axeAvailable = false;
            
            dying = false;
            dyingTimer = 5;
        }

        public bool Init()
        {
            return true;
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public int Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = Strength;
            }
        }

        public int Shield
        {
            get
            {
                return shield;
            }

            set
            {
                shield = value;
            }
        }

        public int Magic
        {
            get
            {
                return magic;
            }

            set
            {
                magic = value;
            }
        }
    }
}
