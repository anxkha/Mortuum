using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Levels
{
    internal class LevelFactory
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }

        public static Level Create(int levelNumber)
        {
            var newLevel = new Level(_content, _graphics) { LevelNumber = levelNumber };
            newLevel.Load();
            return newLevel;
        }
    }
}
