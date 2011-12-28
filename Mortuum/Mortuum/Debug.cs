using System;
using System.IO;

namespace Mortuum
{
    public class Debug
    {
        public static StreamWriter f;

        public Debug()
        {
            f = null;
        }

        public static void Start(string file)
        {
            f = File.AppendText(file);
        }

        public static void Stop()
        {
            f.Close();
        }

        public static void Write(string value)
        {
            f.Write("[" + DateTime.Now.ToString() + "] " + value + "\r\n");
        }
    }
}
