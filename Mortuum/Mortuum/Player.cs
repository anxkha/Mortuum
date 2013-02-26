using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mortuum.Common;
using Mortuum.Enemy;
using Mortuum.Spell;
using Mortuum.Weapon;

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

        private Matrix rotation;
        private Model model;

        public Vector3 Position
        {
            get;
            set;
        }

        public float Direction
        {
            get;
            set;
        }

        public int Health
        {
            get { return health; }
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
            get;
            set;
        }

        public int Shield
        {
            get;
            set;
        }

        public int Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                if (magic < 0) magic = 0;
            }
        }

        public int Score
        {
            get;
            set;
        }

        public int Kills
        {
            get;
            set;
        }

        public bool Dead
        {
            get { return dead; }
        }

        public float WeaponPosition
        {
            get;
            set;
        }

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

            Position = Vector3.Zero;
            Direction = 0.0f;

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
                    dead = true;
            }
            else
            {
                if (health < Settings.PlayerMaxHealth)
                {
                    healthTick += elapsedTime;

                    if (healthTick >= Settings.PlayerHealthRegenDuration)
                    {
                        health++;
                        Magic -= Soul.MAGIC;
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

            rotation = Matrix.CreateRotationX(0.0f) * Matrix.CreateRotationY(MathHelper.ToRadians(Direction)) * Matrix.CreateRotationZ(0.0f);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            if (!loaded) return;

            var transforms = new Matrix[model.Bones.Count];
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
                    e.World = transforms[mesh.ParentBone.Index] * rotation * Matrix.CreateTranslation(Position);
                }

                mesh.Draw();
            }

            graphics.GraphicsDevice.SamplerStates[0] = oldState;
        }

        public void Move(bool forward, float fElapsedTime)
        {
            var tempx = Math.Sin(MathHelper.ToRadians(Direction)) * Settings.PlayerMoveSpeed * fElapsedTime;
            var tempz = Math.Cos(MathHelper.ToRadians(Direction)) * Settings.PlayerMoveSpeed * fElapsedTime;

            Vector3 position = Position;

            if (forward)
            {
                position.X += (float)tempx;
                position.Z += (float)tempz;
            }
            else
            {
                position.X -= (float)tempx;
                position.Z -= (float)tempz;
            }

            Position = position;
        }
    }
}
