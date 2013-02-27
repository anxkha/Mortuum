using System;

namespace Mortuum.Spells
{
    internal class InvalidSpellException : Exception
    {
        public InvalidSpellException(SpellType spellType)
            : base("Invalid spell type: " + spellType.ToString())
        {
        }
    }
}
