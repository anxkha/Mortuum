using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Enemy
{
    internal abstract class EnemyBase : IEnemy
    {
        protected ContentManager Content;
        protected GraphicsDeviceManager Graphics;
        protected Model Model;

        private int _health;
        protected Vector3 _position;
        protected float _direction;
        private bool _dead;

        public int Level
        {
            get;
            set;
        }

        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (_health <= 0)
                {
                    _health = 0;
                    _dead = true;
                }
            }
        }

        public bool Dead
        {
            get { return _dead; }
        }

        public EnemyBase(ContentManager content, GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;

            _position = Vector3.Zero;
            _direction = 0.0f;
            Level = 0;
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
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
            }
        }

        public void Unload()
        {
            Model = null;
        }

        public void Update(float fElapsedTime)
        {
        }

        public void Draw(Matrix view, Matrix projection)
        {
            Matrix[] transforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(transforms);

            var clampState = new SamplerState() { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            var oldState = Graphics.GraphicsDevice.SamplerStates[0];

            Graphics.GraphicsDevice.SamplerStates[0] = clampState;

            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.View = view;
                    e.Projection = projection;
                    e.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(_position);
                }

                mesh.Draw();
            }

            Graphics.GraphicsDevice.SamplerStates[0] = oldState;
        }
    }
}
