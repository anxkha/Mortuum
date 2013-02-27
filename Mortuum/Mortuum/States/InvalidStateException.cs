using System;

namespace Mortuum.States
{
    internal class InvalidStateException : Exception
    {
        public InvalidStateException(GameState gameState)
            : base("Invalid game state: " + gameState.ToString())
        {
        }
    }
}
