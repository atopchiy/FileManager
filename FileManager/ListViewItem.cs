﻿using System;
using System.Collections.Generic;
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
    }
}
