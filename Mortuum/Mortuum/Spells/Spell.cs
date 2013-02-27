using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mortuum.Common;

namespace Mortuum.Spells
{
    delegate void SpellFinishedEvent(Spell spellInstance);
    delegate void SpellTriggeredEvent();

    internal class Spell
    {
        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;
        protected Model _model;

        protected bool _trigger; // Triggered when the internal tick count reaches or exceeds the duration.
        private float tick; // Spell timer for tracking internal spell effects.
        private bool _finished;

        public event SpellFinishedEvent SpellFinishedEvent;

        protected event SpellTriggeredEvent SpellTriggeredEvent;

        public Vector3 Position
        {
            get;
            set;
        }

        public Vector3 Direction
        {
            get;
            set;
        }

        public float Duration // In seconds.
        {
            get;
            set;
        }

        public bool Finished
        {
            get { return _finished; }
            protected set
            {
                _finished = value;
                if (_finished)
                    SpellFinishedEvent(this);
            }
        }

        public Spell(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;

            Position = Vector3.Zero;
            Direction = Vector3.Zero;
            Duration = 0.0f;
            tick = 0.0f;

            _trigger = false;
            _finished = false;
        }

        /// <remarks>
        ///  This base function should be called at the bottom of an overriding function.
        /// </remarks>
        public virtual void Load()
        {
            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
            }
        }

        /// <remarks>
        ///  This base function should be called at the top of an overriding function.
        /// </remarks>
        public virtual void Update(float elapsedTime)
        {
            if (Finished) return;

            tick += elapsedTime;

            if (tick >= Duration)
            {
                tick -= Duration;
                SpellTriggeredEvent();
            }
        }

        public virtual void Draw(Matrix view, Matrix projection)
        {
            if (Finished) return;

            var clampState = new SamplerState() { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            var oldState = _graphics.GraphicsDevice.SamplerStates[0];

            _graphics.GraphicsDevice.SamplerStates[0] = clampState;

            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.View = view;
                    e.Projection = projection;
                    e.World = Matrix.CreateTranslation(Position);

                    mesh.Draw();
                }
            }

            _graphics.GraphicsDevice.SamplerStates[0] = oldState;
        }
    }
}
