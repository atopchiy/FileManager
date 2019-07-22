using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileManager
{
    public class ListViewCollection
    {
        public ListViewCollection()
        {
            ListViews = new List<ListView>();
        }
        public List<ListView> ListViews { get; set; }
        public void Update(ConsoleKeyInfo key)
        {
            var selectedItem = ListViews.FirstOrDefault(x => x.ListViewSelected);
            var selectedItemIndex = ListViews.IndexOf(selectedItem);
            if (key.Key == ConsoleKey.RightArrow)
            { 
                if (selectedItemIndex != ListViews.Count - 1)
                {
                    ListViews[selectedItemIndex].ListViewSelected = false;
                    ListViews[selectedItemIndex + 1].ListViewSelected = true;
                }
            }
            else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (selectedItemIndex != 0)
                    {
                        ListViews[selectedItemIndex].ListViewSelected = false;
                        ListViews[selectedItemIndex - 1].ListViewSelected = true;
                    }
                }
                else
                    selectedItem.Update(key);
        }
    }
}
