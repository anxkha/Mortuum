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
        private ContentManager content;
        private GraphicsDeviceManager graphics;

        private int health;
        private int strength;
        private int shield;
        private int magic;

        private int score;
        private int kills;

        private SpellType activeSpell;
        private WeaponType activeWeapon;

        private bool maceAvailable;
        private bool axeAvailable;

        private bool dying;         // Whether or not to play the dying animation.
        private bool dead;
        private float dyingTick;    // In seconds.
        private bool loaded;
        private float healthTick;
        private float shieldTick;

        private Vector3 position;
        private Vector3 direction;

        private Matrix rotation;
        private Model model;

        public Player()
        {
            health = Settings.PlayerMaxHealth;
            strength = Settings.PlayerMaxStrength;
            shield = Settings.PlayerMaxShield;
            magic = Settings.PlayerMaxMagic;

            score = 0;
            kills = 0;

            activeSpell = SpellType.DragonBreath;
            activeWeapon = WeaponType.Sword;

            maceAvailable = false;
            axeAvailable = false;
            
            dying = false;
            dead = false;
            dyingTick = 0.0f;
            loaded = false;

            position = Vector3.Zero;
            direction = Vector3.Zero;

            healthTick = 0.0f;
            shieldTick = 0.0f;

            WeaponPosition = 0.0f;

            model = null;
            rotation = Matrix.Identity;
        }

        public void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.content = content;
            this.graphics = graphics;

            model = content.Load<Model>("cruentus");

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
            }

            loaded = true;
        }

        public void Unload()
        {
            model = null;
            loaded = false;
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
            if (!loaded) return;

            if (dying)
            {
                dyingTick += elapsedTime;

                if (dyingTick >= Settings.PlayerDyingDuration)
                {
                    dead = true;
                }
            }
            else
            {
                if (health < Settings.PlayerMaxHealth)
                {
                    healthTick += elapsedTime;

                    if (healthTick >= Settings.PlayerHealthRegenDuration)
                    {
                        health++;
                        Magic -= Soul.Magic;
                        healthTick -= Settings.PlayerHealthRegenDuration;
                    }
                }

                if (shield < Settings.PlayerMaxShield)
                {
                    shieldTick += elapsedTime;

                    if (shieldTick >= Settings.PlayerShieldRegenDuration)
                    {
                        shield++;
                        shieldTick -= Settings.PlayerShieldRegenDuration;
                    }
                }
            }

            rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(Direction.Z));
            rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(Direction.Y));
            rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(Direction.X));
        }

        public void Draw(Matrix view, Matrix projection)
        {
            if (!loaded) return;

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            var clampState = new SamplerState() { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            var oldState = graphics.GraphicsDevice.SamplerStates[0];

            graphics.GraphicsDevice.SamplerStates[0] = clampState;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.View = view;
                    e.Projection = projection;
                    e.World = rotation * transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(Position);
                }

                mesh.Draw();
            }

            graphics.GraphicsDevice.SamplerStates[0] = oldState;
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

        public int Kills
        {
            get
            {
                return kills;
            }

            set
            {
                kills = value;
            }
        }

        public bool Dead
        {
            get
            {
                return dead;
            }
        }

        public float WeaponPosition
        {
            get;
            set;
        }
    }
}
