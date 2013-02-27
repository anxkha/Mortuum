using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Enemies
{
    internal class EnemyFactory
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }

        public static Enemy Create(EnemyType enemyType, int level)
        {
            Enemy newEnemy;

            switch (enemyType)
            {
                case EnemyType.Archer:
                    newEnemy = new Archer(_content, _graphics) { Level = level };
                    break;
                case EnemyType.Guard:
                    newEnemy = new Guard(_content, _graphics) { Level = level };
                    break;
                case EnemyType.Wizard:
                    newEnemy = new Wizard(_content, _graphics) { Level = level };
                    break;
                case EnemyType.Soul:
                    newEnemy = new Soul(_content, _graphics);
                    break;
                default:
                    throw new InvalidEnemyException(enemyType);
            }

            newEnemy.Load();
            return newEnemy;
        }
    }
}
