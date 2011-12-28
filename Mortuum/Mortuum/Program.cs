using System;

namespace Mortuum
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Mortuum game = new Mortuum())
            {
                game.Run();
            }
        }
    }
#endif
}

