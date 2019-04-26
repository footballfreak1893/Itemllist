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
            
        }

        //public static void StartMenu()
        //{
        //    Data data = new Data();
        //    Sublist sublist = new Sublist(data);
        //    Console.WriteLine("Checkliste");
        //    Console.WriteLine();
        //    Console.WriteLine("New List [n]");
        //    Console.WriteLine("Open List [o]");
        //    Console.WriteLine("Exit Programm [e]");

        //    var userinput = Console.ReadLine();
        //    userinput.ToLower();

        //    switch (userinput)
        //    {
        //        case "n":
        //            Console.Clear();
        //            sublist.AddSublist(data, sublist.longNamesList);
        //            break;

        //        case "d":
        //            sublist.DeleteSublist(data);
        //            break;

        //        case "b":
        //            Console.Clear();
        //            return;

        //        case "e":
        //            Start.Exit(data);
        //            return;

        //        default:
        //            if (!sublist.shortNamesList.Contains(userinput))
        //            {
        //                Console.Clear();
        //                Console.WriteLine("Error: " + userinput + " does not exists");

        //                break;
        //            }
        //            Console.Clear();
        //            sublist.OpenList(data, userinput);
        //            break;
        //    }
        //}

        public static void StartProgramm(Data data)
        {
            //data.FolderExists();
            Sublist sublist = new Sublist(data);

            string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();
            //Das soll dynamisch passieren
            //Idee: Defaultlist bei Programmstart einlesen, evtl. string speichern der dem Shortname hinzugefügt wird,
            //Wenn Defaultlist geändert wird, wird der str der neuen liste hinzugefügt (Der char soll user nicht bemerken)
            sublist.OpenDefaultList(data);
            //sublist.SubMenu(data, "Ha");
            //display.ShowDetails(data, false, data.dict);
            sublist.SubOverview(data);

            while (true)
            {
                Console.WriteLine("Checkliste ("+version+")");
                Console.WriteLine();
                Console.WriteLine("Defaultlist");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Rename List [rn]");
                Console.WriteLine("Filter [f]");
                Console.WriteLine("Test [t]");
                Console.WriteLine("Sublist [s]");
                //Console.WriteLine("Finished entries [fi]");

                string userinput = Console.ReadLine();

                switch (userinput.ToLower())
                {
                    case "n":
                        Console.Clear();
                        data.AddItem();
                        break;

                    case "e":
                        Console.Clear();
                        Exit(data);
                        break;

                    case "x":
                        Console.Clear();
                        display.ShowDetails(data, false, data.dict);
                        break;

                    case "r":
                        Console.Clear();
                        data.ClearList();
                        break;

                    case "f":
                        Console.Clear();
                        filter.FilterMenu(data);
                        break;

                    case "s":
                        Console.Clear();
                        sublist.SubOverview(data);
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
                        data.AddItem();
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
            data.SaveList(data.path);
            data.SaveId(data.pathId, data.currentid);
            Environment.Exit(1);
        }
    }
}
