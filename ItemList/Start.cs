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
            

           string path = "data";
            data.FileExists();

            while (true)
            {

                Console.WriteLine("Checkliste");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Display entries [s]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Update Item [u]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Count Items [c]");


                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "n":
                        data.AddItem();
                        break;

                    case "s":
                        DisplayAllItems(data);
                        break;
                    case "e":
                        Exit(data);
                        break;

                    case "x":
                        ShowDetails(data);
                        break;

                    case "c":
                        data.CountItems();
                        break;
                    default:
                        DisplayAllItems(data);
                        break;
                }
            }

        }

        public static void Exit(Data data)
        {
            data.SaveList(data.path);
            data.SaveId(data.pathId, data.currentid);
            Environment.Exit(1);

        }

        public static void ShowDetails(Data data)
        {
            DisplayAllItems(data);
            Console.WriteLine("Enter ID to display details [number]");
            string inputid = Console.ReadLine();
            int id = Convert.ToInt16(inputid);
            Item item = data.dict[id];
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Titel: " + item.title);
            Console.WriteLine("Beschreibung: " + item.description);

            Console.WriteLine("Update Item [u]");
            Console.WriteLine("Delete Item [d]");
            string inputvalue = Console.ReadLine();

            if (inputvalue == "u")
            {
                data.UpdateItem(id, item);
            }

            else if (inputvalue == "d")
            {
                data.DeleteItem(id, item);
            }
            else
            {
                return;
            }

        }

        public static void DisplayAllItems(Data data)
        {

            foreach (KeyValuePair<int, Item> entries in data.dict)
            {
                if (entries.Value.isobsolete == false)
                {
                    Console.WriteLine(entries.Value.id + ".) " + "Title " + entries.Value.title);
                }
                else
                {
                    continue;
                }
            }
        }

















    }

}
