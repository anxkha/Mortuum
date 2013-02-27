using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Potion
{
    internal class Potion
    {
        private PotionType type;
        private float acceleration;
        private float duration;     // In seconds.

        public Vector3 Position
        {
            get;
            set;
        }

        public Vector3 Direction
        {
            get;
            set;
        }

        public Potion()
        {
            Position = Vector3.Zero;
            Direction = Vector3.Zero;

            acceleration = 0.0f;
            duration = 0.0f;
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
        }
    }
}
