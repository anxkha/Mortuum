using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    public enum PotionType
    {
        Strength = 1,
        Shield,
        SunHammer
    }

    class Potion
    {
        private PotionType type;

        private Vector3 position;
        private Vector3 direction;
        private float acceleration;
        private float duration;     // In seconds.

        public Potion()
        {
            position = Vector3.Zero;
            direction = Vector3.Zero;

            acceleration = 0.0f;
            duration = 0.0f;
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
