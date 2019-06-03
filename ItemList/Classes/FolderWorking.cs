using ItemList.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ItemList
{
    public class FolderWorking
    {
        ManageListItems data = new ManageListItems();
        
        public static void CreateFolder(string foldername)
        {
            DirectoryInfo di = new DirectoryInfo(foldername);
            di.Create();
            Console.WriteLine("Folder created");
        }
    }
}
