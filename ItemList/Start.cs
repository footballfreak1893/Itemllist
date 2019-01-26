using System;
using System.Collections.Generic;
using System.IO;

namespace ItemList
{
    public class Start
    {
        
        


        static void Main(string[] args)
        {
            StartProgramm();
        }

        public static void StartProgramm()
        {
          Item startitem = new Item();
           string path = "data";
            startitem.FileExists();

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
                        startitem.AddItem();
                        break;

                    case "s":
                        startitem.DisplayAllItems();
                        break;
                    case "e":
                        startitem.Exit();
                        break;
                    
                    case "u":
                        startitem.UpdateItem();
                        break;
                    case "x":
                        startitem.ShowDetails();
                        break;

                    case "c":
                        startitem.CountItems();
                        break;
                    default:
                        startitem.DisplayAllItems();
                        break;
                }
            }

        }

      
        














    }

}
