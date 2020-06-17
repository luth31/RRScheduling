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
        static void AddMenuItem(String str, Action<int> fn) {
            MenuTable.Add(MenuTable.Count+1, (str, fn));
        }

        static void BuildMenuTable() {
            AddMenuItem("Run Scheduler", Scheduler);
            AddMenuItem("Enable debug messages", ToggleDebugMessages);
            AddMenuItem("Enable Idle mode", ToggleIdleMode);
            AddMenuItem("Exit", ExitApp);
        }
        static void ShowMenu() {
            Console.WriteLine("Menu:");
            foreach (var entry in MenuTable) {
                Console.WriteLine("{0}. {1}", entry.Key, entry.Value.Item1);
            }
            ReadSelection();
        }
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
        static void InvalidSelection() {
            Console.WriteLine("Invalid selection!");
            ReadSelection();
        }

        // Selection handlers
        static void Scheduler(int menuID) {
            
        }
        static void ToggleDebugMessages(int menuID) {
            // Toggle the bool
            debugMessages = !debugMessages;
            Console.Clear();
            // Replace the Tuple with another composed of old Action<int> and new string
            String str = (debugMessages ? "Disable" : "Enable") + " debug messages";
            MenuTable[menuID] = (str, MenuTable[menuID].Item2);
            ShowMenu();

        }
        static void ToggleIdleMode(int menuID) {
            // Toggle the bool
            idleMode = !idleMode;
            Console.Clear();
            // Replace the Tuple with another composed of old Action<int> and new string
            String str = (idleMode ? "Disable" : "Enable") + " Idle mode";
            MenuTable[menuID] = (str, MenuTable[menuID].Item2);
            ShowMenu();
        }
        static void ExitApp(int menuID) {
            Environment.Exit(0);
        }
        static bool idleMode = false;
        static bool debugMessages = false;
        static Dictionary<int,(String, Action<int>)> MenuTable;
    }
}
