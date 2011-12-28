using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    class Portal
    {
        public static enum Orientation
        {
            Left = 1,
            Right,
            Top,
            Bottom
        };

        private Vector3 position;
        private Orientation side;

        private float spawnTime;
        private float tickTime;

        public Portal()
        {
            tickTime = 0.0f;
            spawnTime = 0.0f;

            position = Vector3.Zero;
            side = Orientation.Left;
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

        public Orientation Side
        {
            get
            {
                return side;
            }

            set
            {
                side = value;
            }
        }

        public float SpawnTime
        {
            get
            {
                return spawnTime;
            }

            set
            {
                spawnTime = value;
            }
        }
    }
}
