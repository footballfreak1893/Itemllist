﻿using ItemList.Classes;
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
            data.FolderExists();
            string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();

            while (true)
            {
                Console.WriteLine("Checkliste ("+version+")");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Filter [f]");
               // Console.WriteLine("Test [t]");
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
