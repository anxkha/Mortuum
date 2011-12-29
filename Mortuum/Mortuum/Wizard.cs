using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    class Wizard
    {
        private int level;
        private int health;

        private Vector3 position;
        private Vector3 direction;

        public Wizard()
        {
            position = Vector3.Zero;
            direction = Vector3.Zero;
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
        }

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

                if (health <= 0)
                {
                    health = 0;
                    dead = true;
                }
            }
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

    }
}
