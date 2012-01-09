using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    public enum WeaponType
    {
        Sword = 1,
        Mace,
        Axe,
        Hammer
    }

    class Weapon
    {
        private Vector3 position;
        private Vector3 direction;

        public Weapon()
        {
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
        }

        public void Update(float elapsedTime)
        {
        }

        public void Draw()
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
