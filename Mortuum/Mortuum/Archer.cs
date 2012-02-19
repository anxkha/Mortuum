using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    class Archer
    {
        private ContentManager content;
        private GraphicsDeviceManager graphics;

        private int level;
        private int health;

        private Vector3 position;
        private Vector3 direction;

        private bool dead;
        private bool loaded;

        private Model model;

        public Archer()
        {
            position = Vector3.Zero;
            direction = Vector3.Zero;
        }

        public void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            this.content = content;

            model = content.Load<Model>("archer");

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

        public void Update(float fElapsedTime)
        {
            if (!loaded) return;
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
                    e.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(Position);
                }

                mesh.Draw();
            }

            graphics.GraphicsDevice.SamplerStates[0] = oldState;
        }

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
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
                    health = 0;
                    dead = true;
                }
            }
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

        public bool Dead
        {
            get
            {
                return dead;
            }
        }
    }
}
