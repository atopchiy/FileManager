using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public class ListView
    {
        private int prevSelectedIndex;
        private int selectedIndex;
        private bool wasPainted;

        private int x;
        private int y;
        private int height;
        private int scroll;

        public List<int> ColumnsWidth { get; set; }
        public bool ListViewSelected { get; set; }
        public List<ListViewItem> Items { get; set; }
        public ListViewItem SelectedItem => Items[selectedIndex];
        public bool Focused { get; set; }
        public ListView(int x, int y, int height)
        {
            this.x = x;
            this.y = y;
            this.height = height;
        }
        public void Clean()
        {
            selectedIndex = prevSelectedIndex = 0;
            wasPainted = false;
            for(int i = 0; i < Items.Count; i++)
            {
                Console.CursorLeft = x;
                Console.CursorTop = i + y;
                Items[i].Clean(ColumnsWidth, i, x, y);
            }
        }
        public void Render()
        {
            if(!Utility.isPainted)
            Utility.CreateMenuOptions();
            for (int i = 0; i < Math.Min(height,Items.Count); i++)
            {
                int elementIndex = i + scroll;
                if (wasPainted)
                {
                    if (elementIndex != selectedIndex && elementIndex != prevSelectedIndex)
                        continue;
                }
                var item = Items[elementIndex];
                var savedForeground = Console.ForegroundColor;
                var savedBackground = Console.BackgroundColor;
                if (elementIndex == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.CursorLeft = x;
                Console.CursorTop = i + y;
                item.Render(ColumnsWidth, i, x, y);
                Console.ForegroundColor = savedForeground;
                Console.BackgroundColor = savedBackground;

            }
            wasPainted = true;
        }
        internal void Update(ConsoleKeyInfo key)
        {
            if (ListViewSelected)
            {
                if (key.Key == ConsoleKey.DownArrow && selectedIndex + 1 < Items.Count)
                {
                    prevSelectedIndex = selectedIndex;
                    selectedIndex++;
                }

                else if (key.Key == ConsoleKey.UpArrow && selectedIndex - 1 >= 0)
                {
                    prevSelectedIndex = selectedIndex;
                    selectedIndex--;
                }

                if (selectedIndex >= height + scroll)
                {
                    scroll++;
                    wasPainted = false;
                }
                else if (selectedIndex < scroll)
                {
                    scroll--;
                    wasPainted = false;
                }

                else if (key.Key == ConsoleKey.Enter)
                    Selected(this, EventArgs.Empty);
                else if (key.Key == ConsoleKey.Escape)
                    Unselected(this, EventArgs.Empty);
            }

        }

        public event EventHandler Selected;
        public event EventHandler Unselected;
    }
}
