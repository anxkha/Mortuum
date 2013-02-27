using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Spells
{
    internal class SpellFactory
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }

        public static Spell Create(SpellType spellType)
        {
            Spell newSpell;

            switch (spellType)
            {
                case SpellType.Lightning:
                    newSpell = new Lightning(_content, _graphics);
                    break;
                case SpellType.DragonBreath:
                    newSpell = new DragonBreath(_content, _graphics);
                    break;
                case SpellType.Gate:
                    newSpell = new Gate(_content, _graphics);
                    break;
                case SpellType.Apocalypse:
                    newSpell = new Apocalypse(_content, _graphics);
                    break;
                default:
                    throw new InvalidSpellException(spellType);
            }

            newSpell.Load();
            return newSpell;
        }
    }
}
