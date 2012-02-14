using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    public enum SpellType
    {
        DragonBreath = 1,
        Lightning,
        Apocalypse,
        Gate
    }

    class Spell
    {
        private ContentManager content;
        private GraphicsDeviceManager graphics;

        private Model model;

        private SpellType type;

        private Vector3 position;
        private Vector3 direction;
        private float acceleration; // Currently only useful for the Gate spell.
        private float duration;     // In seconds.

        private bool loaded;
        private bool trigger;       // Triggered when the internal tick count reaches or exceeds the duration.
        private bool finished;      // Triggered when the spell has reached completion.

        private bool gateOpened;    // Triggered when the gate has stopped moving.

        private float tick;         // Spell timer for tracking internal spell effects.

        public Spell()
        {
            type = SpellType.DragonBreath;

            position = Vector3.Zero;
            direction = Vector3.Zero;

            acceleration = 0.0f;
            duration = 0.0f;

            tick = 0.0f;

            loaded = false;
            trigger = false;
            finished = false;
        }

        public void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            // This function requires attributes to have been set before being called.
            this.content = content;
            this.graphics = graphics;

            switch (type)
            {
                case SpellType.DragonBreath:
                    if (duration != Settings.DragonBreathDuration) return;

                    model = content.Load<Model>("dragonBreath");

                    break;

                case SpellType.Lightning:
                    if (Vector3.Zero == direction) return;

                    model = content.Load<Model>("lightning");

                    break;

                case SpellType.Gate:
                    if (0.0f == acceleration) return;
                    if (duration != Settings.GateDuration) return;
                    if (direction == Vector3.Zero) return;

                    model = content.Load<Model>("gate");

                    break;
            }

            // Tweak the effect settings of the model's effects.
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

        public void Update(float elapsedTime)
        {
            if (!loaded) return;
            if (finished) return;

            // Advance the basic tick timer and trigger if we hit the general duration.
            tick += elapsedTime;

            if (tick >= duration)
            {
                tick -= duration;
                trigger = true;
            }

            switch (type)
            {
                case SpellType.DragonBreath:
                    if (trigger) finished = true;
                    break;

                case SpellType.Gate:
                    if (!gateOpened)
                    {
                        // TODO: Perform deceleration of the gate before it's spawned.
                    }
                    else
                    {
                        if (trigger)
                        {
                            gateOpened = false;
                            trigger = false;
                            finished = true;
                        }
                    }
                    break;
            }
        }

        public void Draw(Matrix view, Matrix projection)
        {
            if (!loaded) return;
            if (finished) return;

            var clampState = new SamplerState() { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            var oldState = graphics.GraphicsDevice.SamplerStates[0];

            graphics.GraphicsDevice.SamplerStates[0] = clampState;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.View = view;
                    e.Projection = projection;
                    e.World = Matrix.CreateTranslation(Position);

                    mesh.Draw();
                }
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

        public float Acceleration
        {
            get
            {
                return acceleration;
            }

            set
            {
                acceleration = value;
            }
        }

        public float Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }

        public bool Finished
        {
            get
            {
                return finished;
            }
        }
    }
}
