using System;
using System.IO;

namespace Mortuum.Common
{
    internal class Debug
    {
        public static StreamWriter _debugStream;

        public Debug()
        {
            _debugStream = null;
        }

        public static void Start(string file)
        {
            _debugStream = File.AppendText(file);
        }

        public static void Stop()
        {
            _debugStream.Close();
        }

        public static void Write(string value)
        {
            _debugStream.Write("[" + DateTime.Now.ToString() + "] " + value + "\r\n");
        }
    }
}
