using System;
using System.Collections.Generic;
using System.IO;

namespace ItemList
{
    public class Start
    {
        string path = "data.txt";
        List<Item> itemlist = new List<Item>();


        static void Main(string[] args)
        {
            StartProgramm();
        }

        public static void StartProgramm()
        {
          Item startitem = new Item();
            //File Exists
            while (true)
            {

                Console.WriteLine("Checkliste");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Display entries [s]");
                Console.WriteLine("Exit Programm [e]");


                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "n":
                        startitem.AddItem();
                        break;

                    case "s":
                        startitem.DisplayAllItems();
                        break;
                    case "e":
                        startitem.Exit();
                        break;
                    default:
                        startitem.DisplayAllItems();
                        break;
                }
            }

        }

      
        














    }

}
