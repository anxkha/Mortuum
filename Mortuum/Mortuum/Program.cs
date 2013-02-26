namespace Mortuum
{
#if WINDOWS || XBOX
    static class Program
    {
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

