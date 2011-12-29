using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mortuum
{
    class Wizard
    {
        private int level;
        private int health;

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }
    }
}
