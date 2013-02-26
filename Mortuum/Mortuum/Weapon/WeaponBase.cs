using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Weapon
{
    internal class WeaponBase : IWeapon
    {
        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;

        protected Model model;
        protected Matrix rotation;
        protected Vector3 _position;
        protected float _direction;

        public WeaponBase(ContentManager content, GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            _content = content;

            _position = Vector3.Zero;
            _direction = 0.0f;
            rotation = Matrix.Identity;

            model = null;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
        }

        public Vector3 GetPosition()
        {
            return _position;
        }

        public void SetDirection(float direction)
        {
            _direction = direction;
        }

        public float GetDirection()
        {
            return _direction;
        }

        public virtual void Load()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
            }
        }

        public virtual void Unload()
        {
            model = null;
        }

        public virtual void Update(float elapsedTime)
        {
            rotation = Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix.CreateRotationY(MathHelper.ToRadians(_direction)) * Matrix.CreateRotationZ(0.0f);
        }

        public virtual void Draw(Matrix view, Matrix projection)
        {
            var clampState = new SamplerState() { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            var oldState = _graphics.GraphicsDevice.SamplerStates[0];

            _graphics.GraphicsDevice.SamplerStates[0] = clampState;

            var transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.View = view;
                    e.Projection = projection;
                    e.World = transforms[mesh.ParentBone.Index] * rotation * Matrix.CreateTranslation(_position);
                }

                mesh.Draw();
            }

            _graphics.GraphicsDevice.SamplerStates[0] = oldState;
        }
    }
}
