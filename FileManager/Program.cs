using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var listViewCollection = new ListViewCollection();
            InitConsole(listViewCollection);
            listViewCollection.ListViews.First().ListViewSelected = true;
            while (true)
            {
                var key = Console.ReadKey();
                listViewCollection.Update(key);
                for(var i = 0; i < listViewCollection.ListViews.Count; i++)
                    listViewCollection.ListViews[i].Render();
            }
        }
        private static void InitConsole(ListViewCollection listViewCollection)
        {
           
            var view = new ListView(10, 2, height: 20);
            view.ColumnsWidth = new List<int> { 30, 10, 10 };
            view.Items = GetItems("C:\\");
            view.Selected += View_Selected;
            view.Unselected += View_Unselected;
            listViewCollection.ListViews.Add(view);
            var viewSecond = new ListView(70, 2, 20);
            viewSecond.ColumnsWidth = new List<int> { 30, 10, 10 };
            viewSecond.Items = GetItems("C:\\");
            //viewSecond.Selected += View_Selected;
            //viewSecond.Unselected += View_Unselected;
            listViewCollection.ListViews.Add(viewSecond);
        }
        private static List<ListViewItem> GetItems(string path)
        {
            return new DirectoryInfo(path).GetFileSystemInfos()
            .Select(f =>
            new ListViewItem(
                f,
                f.Name,
                f is DirectoryInfo dir ? "<dir>" : f.Extension,
                f is FileInfo file ? file.Length.ToString() : ""))
                .ToList();
         
        }
        private static void View_Unselected(object sender, EventArgs e)
        {
            var view = (ListView)sender;
            var info = view.SelectedItem.State;
            view.Clean();
              if (info is DirectoryInfo dir)
                if(dir.Parent.Parent != null)
                view.Items = GetItems(dir.Parent.Parent.FullName);
        }
        private static void View_Selected(object sender, EventArgs e)
        {
            var view = (ListView)sender;
            var info = view.SelectedItem.State;
            if (info is FileInfo file)
                Process.Start(file.FullName);
            else if (info is DirectoryInfo dir)
            {
                view.Clean();
                view.Items = GetItems(dir.FullName);
            }
        }
    }
}
