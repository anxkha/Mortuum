using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    public enum PortalOrientation
    {
        Left = 1,
        Right,
        Top,
        Bottom
    }

    class Portal
    {
        private Vector3 position;
        private PortalOrientation side;

        private float spawnTime;
        private float spawnTick;
        private bool trigger;

        public Portal()
        {
            spawnTick = 0.0f;
            spawnTime = 0.0f;
            trigger = false;

            position = Vector3.Zero;
            side = PortalOrientation.Left;
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
        }

        public void Update(float elapsedTime)
        {
            spawnTick += elapsedTime;

            if (spawnTick >= spawnTime)
            {
                trigger = true;
                spawnTick -= spawnTime;
            }
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

        public PortalOrientation Side
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
