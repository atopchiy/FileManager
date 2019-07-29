using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    public class ListViewItem
    {
        private readonly string[] columns;
        public object State { get; }
        public ListViewItem(object state, params string[] columns)
        {
            State = state;
            this.columns = columns;
        }

        internal void Render(List<int> columnsWidth, int elementIndex, int listViewX, int listViewY)
        {
           for(int i = 0; i < columns.Length; i++)
            {
                Console.CursorTop = elementIndex + listViewY;
                Console.CursorLeft = columnsWidth.Take(i).Sum() + listViewX;
                Console.Write(GetStringWithLength(columns[i],columnsWidth[i]));
            }
        }

        private string GetStringWithLength(string v1, int maxLength)
        {
            if (v1.Length < maxLength)
                return v1.PadRight(maxLength, ' ');
            else
                return v1.Substring(0, maxLength - 5) + "[...]";
        }

        internal void Clean(List<int> columnsWidth, int i, int x, int y)
        {
            Console.CursorTop = i + y;
            Console.CursorLeft = x;
            Console.Write(new string(' ', columnsWidth.Sum()));
        }
        public void ShowProperties()
        {
           if(State is FileInfo file)
           {
                Console.WriteLine($"Name: {file.Name}");
                Console.WriteLine($"Extension: {file.Extension}");
                Console.WriteLine($"Size: {file.Length}");
                Console.WriteLine($"Path: {file.FullName}");

           }
           else if(State is DirectoryInfo directory)
            {
                Console.WriteLine($"Name: {directory.Name}");
                Console.WriteLine($"Last updated: {directory.LastWriteTimeUtc}");
                Console.WriteLine($"Created at: {directory.CreationTimeUtc }");
                Console.WriteLine($"Path: {directory.FullName }");
            }
        }
    }
}
