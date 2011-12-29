using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
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

        private int score;

        private Spell.SpellType activeSpell;
        private Weapon.WeaponType activeWeapon;

        private bool maceAvailable;
        private bool axeAvailable;

        private bool dying;     // Whether or not to play the dying animation.
        private int dyingTimer; // In seconds.

        private Vector3 position;
        private Vector3 direction;

        public Player()
        {
            maxHealth = health = 10;
            maxStrength = strength = 10;
            maxShield = shield = 5;
            maxMagic = magic = 20;

            score = 0;

            activeSpell = Spell.SpellType.DragonBreath;
            activeWeapon = Weapon.WeaponType.Sword;

            maceAvailable = false;
            axeAvailable = false;
            
            dying = false;
            dyingTimer = 5;

            position = Vector3.Zero;
            direction = Vector3.Zero;
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
        }

        public void Update(float elapsedTime)
        {
        }

        public void Draw()
        {
        }

        public Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Vector3 Direction
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
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

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }
    }
}
