using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Portal
{
    internal class Portal
    {
        private Vector3 position;
        private PortalOrientation side;

        private float spawnTime;
        private float spawnTick;
        private bool trigger;

        public Vector3 Position
        {
            get;
            set;
        }

        public PortalOrientation Side
        {
            get;
            set;
        }

        public float SpawnTime
        {
            get;
            set;
        }

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
    }
}
