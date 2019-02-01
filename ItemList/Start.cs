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
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Count Items [c]");


                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "n":
                        Console.Clear();
                        data.AddItem();
                        break;

                    case "s":
                        Console.Clear();
                        DisplayAllItems(data);
                        break;

                    case "e":
                        Console.Clear();
                        Exit(data);
                        break;

                    case "x":
                        Console.Clear();
                        ShowDetails(data);
                        break;

                    case "c":
                        data.CountItems();
                        break;
                    default:
                        Console.Clear();
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
            int id =  CheckingValuesINT(data, inputid);
            
            
            //int id = Convert.ToInt16(inputid);

            while (!data.dict.ContainsKey(id))
            {
                Console.WriteLine("ID does not exist, try again");
                DisplayAllItems(data);

                Console.WriteLine("Enter ID to display details [number]");
                inputid = Console.ReadLine();
                id = CheckingValuesINT(data, inputid);
                Console.Clear();
            }

            Item item = data.dict[id];
            Console.Clear();
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Title: " + item.title);
            Console.WriteLine("Description: " + item.description);
            Console.WriteLine();
            Console.WriteLine("Update Item [u]");
            Console.WriteLine("Delete Item [d]");
            string inputvalue = Console.ReadLine();

            switch (inputvalue)
            {
                case "u":
                    data.UpdateItem(id, item);
                    Console.Clear();
                    break;

                case "d":
                    Console.Clear();
                    data.DeleteItem(id, item);
                    break;

                default:
                    Console.Clear();
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

        public static int CheckingValuesINT(Data data, string input)
        {
            bool canconvert = false;
            Int16 integer = 0;

            while (canconvert == false)
            {
                canconvert = Int16.TryParse(input, out integer);
                if (canconvert == false)
                {
                    Console.WriteLine("Incorrect data type, enter a whole number");
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }

            }

            return integer;
        }

        public static double CheckingValuesDouble(Data data, string input)
        {

            bool canconvert = false;
            double doublevalue = 0;

            while (canconvert == false)
            {
                canconvert = double.TryParse(input, out doublevalue);
                if (canconvert == false)
                {
                    Console.WriteLine("Incorrect data type, enter a numeric value");
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }

            }
            return doublevalue;
        }

        public static double CheckingValuesChar(Data data, string input)
        {

            bool canconvert = false;
            char charvalue = '0';

            while (canconvert == false)
            {
                canconvert = char.TryParse(input, out charvalue);
                if (canconvert == false)
                {
                    Console.WriteLine("Incorrect data type, enter a char value [example a or 1]");
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }

            }
            return charvalue;
        }
















    }

}
