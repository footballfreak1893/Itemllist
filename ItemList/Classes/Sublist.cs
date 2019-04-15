using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList.Classes
{
    public class Sublist
    {
        public void SubMenu()
        {

            Console.WriteLine("Sublists");

            Display display = new Display();
            Filter filter = new Filter();
            Data data = new Data();
             data. sub = data.FolderExists(data.folderSub, data.pathSub, data.pathIdSub, data.currentidDefaultSub, data.StartIdSub, data.sub);

            while (true)
            {
                Console.WriteLine("Sublist");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Filter [b]");
                // Console.WriteLine("Test [t]");
                //Console.WriteLine("Finished entries [fi]");

                string userinput = Console.ReadLine();

                switch (userinput.ToLower())
                {
                    case "n":
                        Console.Clear();
                        data.AddItem(data.sub, data.currentidDefaultSub, data.pathIdSub, data.pathSub);
                        break;

                    case "e":
                        Console.Clear();
                        Start.Exit(data);
                        break;

                    case "x":
                        Console.Clear();
                        display.ShowDetails(data, data.sub);
                        break;

                    case "r":
                        Console.Clear();
                        data.ClearList();
                        break;

                    case "f":
                        Console.Clear();
                        return;
                        break;

                    //case "t":
                    //    Console.Clear();
                    //    Test();

                    //  break;

                    //case "fi":
                    //    Console.Clear();
                    //    display.EntriesSetFinished(data);
                    //    break;

                    default:
                        Console.Clear();
                        display.DisplayAllItems(data);
                        Console.Clear();
                        break;
                }
            }
        }
    }
}

