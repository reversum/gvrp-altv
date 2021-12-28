using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVRPALTV.Logger
{
    public static class Logger
    {
        public static void info(string text)
        {
            Console.WriteLine("[" + ConsoleColor.Blue + "INFORMATION" + ConsoleColor.White + "] " + ConsoleColor.Yellow + text);

        }
        public static void error(string text)
        {
            Console.WriteLine("[" + ConsoleColor.Red + "ERROR" + ConsoleColor.White + "] " + ConsoleColor.Yellow + text);
        }
        public static void debug(string text)
        {
            Console.WriteLine("[" + ConsoleColor.Black + "DEBUG" + ConsoleColor.White + "] " + ConsoleColor.Yellow + text);
        }
    }
}
