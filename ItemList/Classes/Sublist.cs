using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ItemList.Classes
{
    class Sublist
    {
       // Data data = new Data();

        public Sublist(Data data)
        {
            
        }

        string listcollectionPath = @"ListCollection\collection.txt";
        //Erweietrungen
        public void SubMenu(Data data)
        {
            string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();
            //Data data = new Data();
            data.CheckListtype("s");
            data.FolderExists();
            

            while (true)
            {
                Console.WriteLine("Sublist Menu:");
                Console.WriteLine();
                Console.WriteLine("Sublist");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Filter [f]");
                Console.WriteLine("back to Main[b]");
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
                        Start.Exit(data);
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

                    case "b":
                        Console.Clear();
                        return;

                    //case "fi":
                    //    Console.Clear();
                    //    display.EntriesSetFinished(data);
                    //    break;

                    default:
                        Console.Clear();
                        return;
                }

            }
        }

        public void SubOverview(Data data)
        {
            Console.WriteLine("Avaible lists");
            Console.WriteLine();
            Console.WriteLine("Coose a list");
            Console.WriteLine("New sublist");

          
            List<string> listCollection = new List<string>();
            listCollection.Add("Sub1");
            listCollection.Add("Sub2");
            listCollection.Add("Sub3");

            // File.WriteAllLines(listcollectionPath, listCollection);
            foreach (var item in listCollection)
            {
                Console.WriteLine(item + "  [ "+item[0]+item[3]+"]");
            }
            var userinput = Console.ReadLine();

            //switch (userinput)
            //{
            //    case "s1":
            //        //
            //        break;

            //    case "s2":
            //        //
            //        break;

            //    case "s3":
            //        //
            //        break;

            //    default:
            //        break;
            //}

            data.CheckListtype(userinput);
            data.FolderExists();

        }
    }
}

