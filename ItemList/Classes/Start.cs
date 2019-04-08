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
            Data data = new Data();
            StartProgramm(data);
            Display display = new Display();
            
        }

        public static void StartProgramm(Data data)
        {
            string path = "data";
            //Defaullist
            data.FolderExists(data.folderDefault, data.pathDefault, data.pathIdDefault, "dict");
            string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();
            Sublist sublist = new Sublist();

            while (true)
            {
                Console.WriteLine("Checkliste ("+version+")");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Filter [f]");
                Console.WriteLine("Test [t]");
                Console.WriteLine("Count [o]");
                //Console.WriteLine("Finished entries [fi]");

                string userinput = Console.ReadLine();

                switch (userinput.ToLower())
                {
                    case "n":
                        Console.Clear();
                        data.AddItem(data.dict, data.currentidDefault, data.pathIdDefault, data.pathDefault );
                        break;

                    case "e":
                        Console.Clear();
                        Exit(data);
                        break;

                    case "x":
                        Console.Clear();
                        display.ShowDetails(data, data.dict);
                        break;

                    case "r":
                        Console.Clear();
                        data.ClearList();
                        break;

                    case "f":
                        Console.Clear();
                        filter.FilterMenu(data);
                        break;

                    case "t":
                        Console.Clear();
                        sublist.SubMenu();
                        break;

                    case "o":
                        Console.Clear();
                        data.CountItems();


                        break;

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

        public static void ListContainsEntries(Data data)
        {
            var counterentries = data.dict.Count;

            if (counterentries != 0)
            {
                return;
            }

            else
            {
                Console.WriteLine("The list does not contain any entries");
                Console.WriteLine("New entry [n]");
                Console.WriteLine("Exit Programm [e]");

                string inputvalue = Console.ReadLine();

                switch (inputvalue)
                {
                    case "n":
                        //data.AddItem();
                        Console.Clear();
                        break;

                    case "e":
                        Console.Clear();
                        Exit(data);
                        break;

                    default:
                        Console.Clear();
                        return;
                }
            }
        }

        public static void Exit(Data data)
        {
            data.SaveList(data.pathDefault, data.dict);
            //data.SaveId(data.pathIdDefault, data.currentidDefault); --> Hier wird Fehler mit ID geworfen 
            Environment.Exit(1);
        }
    }
}
