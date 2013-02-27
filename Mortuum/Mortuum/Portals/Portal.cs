using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Portals
{
    delegate void TriggerEvent(Portal portalInstance);

    internal class Portal
    {
        private ContentManager _content;
        private GraphicsDeviceManager _graphics;

        private float _spawnTick;

        public event TriggerEvent TriggerEvent;

        public Vector3 Position
        {
            get;
            set;
        }

        public PortalOrientation Orientation
        {
            get;
            set;
        }

        public float SpawnTime
        {
            get;
            set;
        }

        public Portal(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;

            _spawnTick = 0.0f;
            SpawnTime = 0.0f;

            Position = Vector3.Zero;
            Orientation = PortalOrientation.Left;
        }

        /// <remarks>
        ///  This base function should be called at the bottom of an overriding function.
        /// </remarks>
        public void Load()
        {
        }

        public void Update(float elapsedTime)
        {
            _spawnTick += elapsedTime;

            if (_spawnTick >= SpawnTime)
            {
                _spawnTick -= SpawnTime;
                TriggerEvent(this);
            }
        }

        public void Draw()
        {
        }
    }
}
