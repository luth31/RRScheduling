using System;

namespace RRScheduling {
    class Debug {
        public static void Write(String str, params object[] parameters) {
            if (!debugMessages)
                return;
            Console.Write(str, parameters);
        } 

        public static void WriteLine(String str, params object[] parameters) {
            if (!debugMessages)
                return;
            Console.WriteLine(str, parameters);
        }
        static public bool debugMessages = false;
    }
    // Colored console wrapper
    class CConsole {
        public static void WriteLine(ConsoleColor color, String str, params object[] parameters) {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str, parameters);
            Console.ForegroundColor = temp;
        }
        public static void Write(ConsoleColor color, String str, params object[] parameters) {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(str, parameters);
            Console.ForegroundColor = temp;
        }
    }
    // Colored debug wrapper
    class CDebug {
        public static void WriteLine(ConsoleColor color, String str, params object[] parameters) {
            if (!Debug.debugMessages)
                return;
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str, parameters);
            Console.ForegroundColor = temp;
        }
        public static void Write(ConsoleColor color, String str, params object[] parameters) {
            if (!Debug.debugMessages)
                return;
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(str, parameters);
            Console.ForegroundColor = temp;
        }
    }
}