using System;

namespace Mortuum.Enemies
{
    internal class InvalidEnemyException : Exception
    {
        public InvalidEnemyException(EnemyType enemyType)
            : base("Invalid enemy type: " + enemyType.ToString())
        {
        }
    }
}
