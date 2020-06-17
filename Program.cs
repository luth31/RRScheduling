using System;
using System.Collections.Generic;

namespace RRScheduling
{
    class Program
    {
        static Dictionary<int,(String, Func<int>)> MenuTable;
        static void Main(string[] args)
        {
            MenuTable = new Dictionary<int,(String, Func<int>)>();
            BuildMenuTable();
            //ShowMenu();
            Scheduler s = new Scheduler(3);
            s.AddTask(new Task("P1", 0, 10));
            s.AddTask(new Task("P2", 1, 4));
            s.AddTask(new Task("P3", 2, 5));
            s.AddTask(new Task("P4", 3, 3));
            /*s.AddTask(new Task("P1", 0, 4));
            s.AddTask(new Task("P2", 0, 3));
            s.AddTask(new Task("P3", 0, 5));*/
            s.Begin();
        }
        static int Test1() {
            Console.WriteLine("test1");
            return 0;
        }
    
        static int Test2() {
            Console.WriteLine("test2");
            return 0;
        }
        static void AddMenuItem(String str, Func<int> fn) {
            MenuTable.Add(MenuTable.Count, (str, fn));
        }
        static void BuildMenuTable() {
            AddMenuItem("Test", Test1);
            AddMenuItem("Test2", Test2);
            AddMenuItem("Exit", () => {
                Environment.Exit(0);
                return 0; });
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
                MenuTable[selector].Item2();
                ShowMenu();
            }
            else
                InvalidSelection();
        }
        static void InvalidSelection() {
            Console.WriteLine("Invalid selection!");
            ReadSelection();
        }
    }
}
