using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Spells
{
    internal class Lightning : Spell
    {
        public Lightning(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            SpellTriggeredEvent += SpellTriggered;
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            _model = _content.Load<Model>("lightning");
            base.Draw(view, projection);
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);

            if (Finished) return;
        }

        private void SpellTriggered()
        {
            Finished = true;
        }
    }
}
