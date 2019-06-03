using ItemList.Classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace ItemList
{

    public class Start
    {
        static void Main(string[] args)
        {
            ManageListItems data = new ManageListItems();
            StartProgramm(data);
        }

        public static void StartProgramm(ManageListItems data)
        {
            ManageLists sublist = new ManageLists(data);

            //string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();
            sublist.ListOverview(data);
        }

        public static void Exit(ManageListItems data)
        {
            data.SaveList(data.pathList);
            data.SaveId(data.pathId, data.currentid);
            Environment.Exit(1);
        }
    }
}
