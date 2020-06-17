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
            AddMenuItem((!idleMode ? "Enable" : "Disable")+" Idle mode", ToggleIdleMode);
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

        // =========== Selection handlers ===========
        // Most of them self explanatory
        static void Scheduler(int menuID) {
            
        }
        static void ToggleDebugMessages(int menuID) {
            // Toggle the bool
            Debug.debugMessages = !Debug.debugMessages;
            Console.Clear();
            // Replace the Tuple with another composed of old Action<int> and new string
            String str = (Debug.debugMessages ? "Disable" : "Enable") + " debug messages";
            MenuTable[menuID] = (str, MenuTable[menuID].Item2);
        }
        static void ToggleIdleMode(int menuID) {
            // Toggle the bool
            idleMode = !idleMode;
            Console.Clear();
            // Replace the Tuple with another composed of old Action<int> and new string
            String str = (idleMode ? "Disable" : "Enable") + " Idle mode";
            MenuTable[menuID] = (str, MenuTable[menuID].Item2);
        }
        static void ExitApp(int menuID) {
            Environment.Exit(0);
        }
        // =========== Members ===========
        // Simple boolean that will be passed to Scheduler; determines if Scheduler will idle while waiting for arrival of tasks or execute tasks sooner (no idle time)
        static bool idleMode = true;
        // Dictionary with an int as key (menuID) and a Tuple composed of a String (MenuItem text) and an Action<int>
        static Dictionary<int,(String, Action<int>)> MenuTable;
    }
}
