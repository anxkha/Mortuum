using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Spells
{
    internal class Gate : Spell
    {
        private bool _gateOpened;    // Triggered when the gate has stopped moving.

        public float Acceleration
        {
            get;
            set;
        }

        public Gate(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            SpellTriggeredEvent += SpellTriggered;
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            _model = _content.Load<Model>("gate");
            base.Draw(view, projection);
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);

            if (Finished) return;

            if (!_gateOpened)
            {
                // TODO: Perform deceleration of the gate before it's spawned.
            }
        }

        private void SpellTriggered()
        {
            _gateOpened = false;
            Finished = true;
        }
    }
}
