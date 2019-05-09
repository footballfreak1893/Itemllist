using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList.Classes
{
    class Display
    {
        public void DisplayListItems(Data data)
        {
            Start.ListContainsEntries(data);

            foreach (KeyValuePair<int, Item> entries in data.dict)
            {
                if (entries.Value.isfinished == true)
                {
                    Console.WriteLine(entries.Value.id + ".) " + entries.Value.title + " ->FINISHED<-");
                }

                else
                {
                    Console.WriteLine(entries.Value.id + ".) " + entries.Value.title);
                }
            }
        }

        public void ShowEntryDetails(Data data, Dictionary<int, Item> dict)
        {
            Start.ListContainsEntries(data);
            DisplayListItems(data);

            Console.WriteLine("Enter ID to display details [number]");
            string inputid;
            inputid = Console.ReadLine();

            if (inputid == "" || inputid == null)
            {
                return;
            }
            int id = CheckingNumbers.CheckingValuesINT(inputid);

            while (!data.dict.ContainsKey(id))
            {
                Console.WriteLine("ID does not exist, try again");
                DisplayListItems(data);

                Console.WriteLine("Enter ID to display details [number]");
                inputid = Console.ReadLine();
                id = CheckingNumbers.CheckingValuesINT(inputid);
                Console.Clear();
            }

            Item item = data.dict[id];
            Console.Clear();
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Title: " + item.title);
            Console.WriteLine("Description: " + item.description);
            Console.WriteLine("Createdate: " + item.createdate.ToShortDateString());
            Console.WriteLine("Priority: " + item.priority);
            Console.WriteLine("Enddate: " + item.enddate.ToShortDateString());
            
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
                    Start.Exit(data);
                    break;

                default:
                    Console.Clear();
                    return;
            }
        }
    }
}
