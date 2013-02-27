using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Spells
{
    internal class DragonBreath : Spell
    {
        public DragonBreath(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            SpellTriggeredEvent += SpellTriggered;
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            _model = _content.Load<Model>("dragonBreath");
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
