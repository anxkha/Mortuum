using System;

namespace Mortuum.Enemy
{
    internal class InvalidEnemyException : Exception
    {
        public InvalidEnemyException(EnemyType enemyType)
            : base("Invalid enemy type: " + enemyType.ToString())
        {
        }
    }
}
