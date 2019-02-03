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

                Console.WriteLine("Checkliste (v 1.0)");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Display entries [s]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");


                string userinput = Console.ReadLine();

                switch (userinput.ToLower())
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

                    case "r":
                        Console.Clear();
                        data.ClearList();
                        

                        break;
                    default:
                        Console.Clear();
                        DisplayAllItems(data);
                        Console.Clear();
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
            string inputid;
            inputid = Console.ReadLine();
            if (inputid == "")
            {
                Console.Clear();
                return;
            }
            int id =  CheckingValuesINT(inputid);
            
            
            //int id = Convert.ToInt16(inputid);

            while (!data.dict.ContainsKey(id))
            {
                Console.WriteLine("ID does not exist, try again");
                DisplayAllItems(data);

                Console.WriteLine("Enter ID to display details [number]");
                inputid = Console.ReadLine();
                id = CheckingValuesINT(inputid);
                Console.Clear();
            }

            Item item = data.dict[id];
            Console.Clear();
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Title: " + item.title);
            Console.WriteLine("Description: " + item.description);
            Console.WriteLine("Createdate: " + item.createdate);
            if (item.enddate.Equals("2000, 01, 01")) //--> Checking Date Value
            {
                Console.WriteLine("Enddate: ");
            }

            else
            {
                Console.WriteLine("Enddate: " + item.enddate);
            }
            Console.WriteLine();
            Console.WriteLine("Update Item [u]");
            Console.WriteLine("Delete Item [d]");
            Console.WriteLine("Set entry finish [f]");
            Console.WriteLine("Exit Programm [e]");
            string inputvalue = Console.ReadLine();

            switch (inputvalue)
            {
                case "u":
                    Console.Clear();
                    data.UpdateItem(id, item);
                    Console.Clear();
                    break;

                case "d":
                    Console.Clear();
                    data.DeleteItem(id, item);
                    break;

                case "f":
                    Console.Clear();
                    item.isfinished = true;
                    Console.WriteLine("Set is finsihed"); //--> Evtl eigene Methode
                    data.SaveList(data.path);
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

        public static void DisplayAllItems(Data data)
        {
            ListContainsEntries(data);
            
            foreach (KeyValuePair<int, Item> entries in data.dict)
            {
                if (entries.Value.isfinished == true)
                {
                    Console.WriteLine(entries.Value.id + ".) " +entries.Value.title +" ->FINISHED<-");
                }
               
                else
                {
                    Console.WriteLine(entries.Value.id + ".) " +entries.Value.title);
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

        public static int CheckingValuesINT( string input)
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
