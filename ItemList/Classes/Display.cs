using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList.Classes
{
    class Display
    {
        Start start = new Start();

        public void AllItems(Data data)
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

        public void ShowDetails(Data data)
        {
            AllItems(data);
            Console.WriteLine("Enter ID to display details [number]");
            string inputid;
            inputid = Console.ReadLine();
            if (inputid == "")
            {
                Console.Clear();
                return;
            }
            int id = CheckingNumbers.CheckingValuesINT(inputid);


            //int id = Convert.ToInt16(inputid);

            while (!data.dict.ContainsKey(id))
            {
                Console.WriteLine("ID does not exist, try again");
                AllItems(data);

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
            if (item.enddate.Equals("0001, 01, 01")) //--> Checking Date Value
            {
                Console.WriteLine("Enddate: ");
            }

            else
            {
                Console.WriteLine("Enddate: " + item.enddate.ToShortDateString());
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
                    Start.Exit(data);
                    break;

                default:
                    Console.Clear();
                    return;
            }
        }
    }
}
