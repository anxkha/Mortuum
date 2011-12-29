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
        public const int MaxHealth = 5;
        public const int MaxShield = 3;
        public const int MaxMagic = 156;
        public const int MaxStrength = 10;

        private int health;
        private int strength;
        private int shield;
        private int magic;

        private int score;

        private Spell.SpellType activeSpell;
        private Weapon.WeaponType activeWeapon;

        private bool maceAvailable;
        private bool axeAvailable;

        private bool dying;         // Whether or not to play the dying animation.
        private bool dead;
        private float dyingTick;    // In seconds.
        private const float dyingDuration = 6.0f;

        private float healthTick;
        private const float healthRegenDuration = 6.0f;

        private float shieldTick;
        private const float shieldRegenDuration = 3.0f;

        private Vector3 position;
        private Vector3 direction;

        public Player()
        {
            health = MaxHealth;
            strength = MaxStrength;
            shield = MaxShield;
            magic = MaxMagic;

            score = 0;

            activeSpell = Spell.SpellType.DragonBreath;
            activeWeapon = Weapon.WeaponType.Sword;

            maceAvailable = false;
            axeAvailable = false;
            
            dying = false;
            dead = false;
            dyingTick = 0.0f;

            position = Vector3.Zero;
            direction = Vector3.Zero;

            healthTick = 0.0f;
            shieldTick = 0.0f;
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
        }

        public void Damage(int amount)
        {
            if (shield < amount)
            {
                amount -= shield;
                Shield = 0;

                Health -= amount;
            }
            else
            {
                Health -= amount;
            }
        }

        public void Update(float elapsedTime)
        {
            if (dying)
            {
                dyingTick += elapsedTime;

                if (dyingTick >= dyingDuration)
                {
                    dead = true;
                }
            }
            else
            {
                if (health < MaxHealth)
                {
                    healthTick += elapsedTime;

                    if (healthTick >= healthRegenDuration)
                    {
                        health++;
                        Magic -= Soul.Magic;
                        healthTick -= healthRegenDuration;
                    }
                }

                if (shield < MaxShield)
                {
                    shieldTick += elapsedTime;

                    if (shieldTick >= shieldRegenDuration)
                    {
                        shield++;
                        shieldTick -= shieldRegenDuration;
                    }
                }
            }
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

                if (health <= 0)
                {
                    dying = true;
                    health = 0;
                }
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

                if (magic < 0) magic = 0;
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

        public bool Dead
        {
            get
            {
                return dead;
            }
        }
    }
}
