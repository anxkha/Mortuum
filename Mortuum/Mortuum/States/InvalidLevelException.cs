using System;

namespace Mortuum.States
{
    internal class InvalidLevelException : Exception
    {
        public InvalidLevelException(int level)
            : base("Invalid level " + level)
        {
        }
    }
}
