using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Enemies
{
    delegate void DeathEvent(Enemy enemyInstance);

    internal abstract class Enemy
    {
        protected ContentManager Content;
        protected GraphicsDeviceManager Graphics;
        protected Model Model;

        private int _health;
        protected Vector3 _position;
        protected float _direction;

        public event DeathEvent DeathEvent;

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
                    Dead = true;
                    DeathEvent(this);
                }
            }
        }

        public bool Dead
        {
            get;
            private set;
        }

        public Enemy(ContentManager content, GraphicsDeviceManager graphics)
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

        /// <remarks>
        ///  This base function should be called at the bottom of an overriding function.
        /// </remarks>
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
