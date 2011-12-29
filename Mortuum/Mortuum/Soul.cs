using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    class Soul
    {
        public const int Score = 50;
        public const int Magic = 13;

        private Vector3 position;
        private Vector3 direction;

        public Soul()
        {
            position = Vector3.Zero;
            direction = Vector3.Zero;
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
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
