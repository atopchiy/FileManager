using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public static class Utility
    {
        public static void CreateMenuOptions()
        {
            var options = "F1 - copy | F2 - cut | F3 - paste | F4 - create folder | F5 - list of discs | F6 - properties | F7 - rename";
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 1;
            Console.Write(options);
            // Restore previous position
            Console.SetCursorPosition(x, y);
           
        }
    }
}
