using System;
using System.Collections.Generic;

namespace RRScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuTable = new Dictionary<int,(String, Action<int>)>();
            BuildMenuTable();
            Console.Clear();
            ShowMenu();
        }
        // Add MenuItem to MenuTable using Count+1 as the ID (so the menu begins from 1 instead of 0) and a Tuple made out of String str and Action<int> fn
        static void AddMenuItem(String str, Action<int> fn) {
            MenuTable.Add(MenuTable.Count+1, (str, fn));
        }
        // Function used to define the items of the menu; created to keep Main as clean as possible
        static void BuildMenuTable() {
            AddMenuItem("Run Scheduler", Scheduler);
            AddMenuItem((!Debug.debugMessages ? "Enable" : "Disable") + " debug messages", ToggleDebugMessages);
            AddMenuItem("Exit", ExitApp);
        }
        // Show menu items
        static void ShowMenu() {
            Console.WriteLine("Menu:");
            foreach (var entry in MenuTable) {
                Console.WriteLine("{0}. {1}", entry.Key, entry.Value.Item1);
            }
            ReadSelection();
        }
        // Read line then Try to parse to int; if exception is thrown call InvalidSelection(); otherwise call Action<int> handler for the menuID given in selector
        static void ReadSelection() {
            Console.Write("Selector: ");
            string input = Console.ReadLine();
            int selector = -1;
            try {
                Int32.TryParse(input, out selector);
            }
            catch {
                InvalidSelection();
            }
            if (MenuTable.ContainsKey(selector)) {
                MenuTable[selector].Item2(selector);
                ShowMenu();
            }
            else
                InvalidSelection();
        }
        // Helper function to let the user know about his wrong selection and Reread selection
        static void InvalidSelection() {
            Console.WriteLine("Invalid selection!");
            ReadSelection();
        }
        // Helper function to read int's; fallback to InvalidSelection in case TryParse throws an exception
        static int ReadInt() {
            int temp = 0;
            try {
                String buffer = Console.ReadLine();
                int.TryParse(buffer, out temp);
            }
            catch {
                InvalidSelection();
            }
            return temp;
        }

        // =========== Selection handlers ===========
        // Most of them self explanatoryS
        static void Scheduler(int menuID) {
            Console.Clear();
            int Pmin; // Process min
            int Pmax; // Process max
            int Smin; // Start min
            int Smax; // Start max
            int Bmin; // Burst min
            int Bmax; // Burst max
            int Q;
            Console.Write("Pmin: ");
            Pmin = ReadInt();
            Console.Write("Pmax: ");
            Pmax = ReadInt();
            Console.Write("Smin: ");
            Smin = ReadInt();
            Console.Write("Smax: ");
            Smax = ReadInt();
            Console.Write("Bmin: ");
            Bmin = ReadInt();
            Console.Write("Bmin: ");
            Bmax = ReadInt();
            Console.Write("Q: ");
            Q = ReadInt();
            int P = RNG.Next(Pmin, Pmax);
            Scheduler scheduler = new Scheduler(Q);
            for (int i = 0; i < P; ++i) {
                int Start = RNG.Next(Smin, Smax);
                int Burst = RNG.Next(Bmin, Bmax);
                scheduler.AddTask(new Task(new String("P"+i), Start, Burst));
            }
            scheduler.Begin();
        }
        static void ToggleDebugMessages(int menuID) {
            // Toggle the bool
            Debug.debugMessages = !Debug.debugMessages;
            Console.Clear();
            // Replace the Tuple with another composed of old Action<int> and new string
            String str = (Debug.debugMessages ? "Disable" : "Enable") + " debug messages";
            MenuTable[menuID] = (str, MenuTable[menuID].Item2);
        }
        // Exit handler
        static void ExitApp(int menuID) {
            Environment.Exit(0);
        }
        // =========== Members ===========
        // Dictionary with an int as key (menuID) and a Tuple composed of a String (MenuItem text) and an Action<int>
        static Dictionary<int,(String, Action<int>)> MenuTable;
        // RNG engine
        static Random RNG = new Random();
    }
}
