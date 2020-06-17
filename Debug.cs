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
}