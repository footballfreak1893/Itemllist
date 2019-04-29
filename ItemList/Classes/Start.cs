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

        public static void StartProgramm(Data data)
        {
            Sublist sublist = new Sublist(data);

            //string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();

            if (File.Exists(sublist.ListShortNamesPath))
            {
                sublist.OpenDefaultList(data);
            }

            sublist.SubOverview(data);
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
