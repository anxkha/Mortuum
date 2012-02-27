using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    public enum WeaponType
    {
        Sword = 1,
        Mace,
        Axe,
        Hammer
    }

    class Weapon
    {
        private ContentManager content;
        private GraphicsDeviceManager graphics;

        private WeaponType type;

        private bool loaded;

        private Model model;
        private Matrix rotation;

        public Weapon()
        {
            type = WeaponType.Sword;

            Position = Vector3.Zero;
            Direction = 0.0f;

            rotation = Matrix.Identity;

            model = null;
            loaded = false;
        }

        public void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            // This function requires attributes to have been set before being called.

            this.graphics = graphics;
            this.content = content;

            switch (type)
            {
                case WeaponType.Sword:
                    model = content.Load<Model>("sword");
                    break;
            }

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

        public void Update(float elapsedTime)
        {
            if (!loaded) return;

            rotation = Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix.CreateRotationY(MathHelper.ToRadians(Direction)) * Matrix.CreateRotationZ(0.0f);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            if (!loaded) return;

            var clampState = new SamplerState() { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            var oldState = graphics.GraphicsDevice.SamplerStates[0];

            graphics.GraphicsDevice.SamplerStates[0] = clampState;

            var transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

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

        public WeaponType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}
