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

        public void DisplayAllItems(Data data)
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

        public void ShowDetails(Data data, bool deactivateDisplayAllItem, Dictionary<int, Item> dict)
        {
            if (deactivateDisplayAllItem == false)
            {
                DisplayAllItems(data);
            }

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
                DisplayAllItems(data);

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

        //public void EditEntry(Data data, int id, Item item)
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("Update Item [u]");
        //    Console.WriteLine("Delete Item [d]");
        //    Console.WriteLine("Set entry finish [f]");
        //    Console.WriteLine("Exit Programm [e]");
        //    string inputvalue = Console.ReadLine();

        //    switch (inputvalue)
        //    {
        //        case "u":
        //            Console.Clear();
        //            data.UpdateItem(id, item);
        //            Console.Clear();
        //            break;

        //        case "d":
        //            Console.Clear();
        //            data.DeleteItem(id, item);
        //            break;

        //        case "f":
        //            Console.Clear();
        //            item.isfinished = true;
        //            Console.WriteLine("Set is finsihed"); //--> Evtl eigene Methode
        //            data.SaveList(data.path);
        //            break;

        //        case "e":
        //            Console.Clear();
        //            Start.Exit(data);
        //            break;

        //        default:
        //            Console.Clear();
        //            return;
        //    }
        //}

        public void FilterMenu(Data data)
        {
                Console.WriteLine("Filter attributes");
                Console.WriteLine();
                Console.WriteLine("Show entries which are finished [f]");
                Console.WriteLine("enddate < today [e<]");
                Console.WriteLine("enddate > today [e>]");
                Console.WriteLine("enddate equals today [e=]");
                string inputvalue = Console.ReadLine();

                if (inputvalue == "")
                {
                    Console.Clear();
                    return;
                }
                Console.Clear();
                shortDict(data, inputvalue);
        }

        public Dictionary<int, Item> EntriesSetFinished(Data data)
        {
            data.dict = data.dict.Where(x => (x.Value.isfinished == true)).ToDictionary(x => x.Key, i => i.Value);
            return data.dict;
        }

        //Hier weiter
        public void shortDict(Data data, string sortattribute)
        {
            bool endate = false;
            bool isfinished = false;
            var array = sortattribute.ToCharArray();


            if (array[0] == 'e')
            {
                endate = true;

                if (array[1] == '<')
                {
                    data.dict = ShortEnddate(data, '<');
                }
                else if (array[1] == '>')
                {
                    data.dict = ShortEnddate(data, '>');
                }
                else
                {
                    data.dict = ShortEnddate(data, '=');
                }

            }
            if (array[0] == 'f')
            {
                isfinished = true;
                data.dict = EntriesSetFinished(data);
            }

            //ShowDetails(data, false, data.dict);

            foreach (KeyValuePair<int, Item> entries in data.dict)
            {
                if (endate == true)
                {
                    Console.WriteLine(entries.Value.id + ".) " + entries.Value.title + ": --> " + entries.Value.enddate.Date);
                }

                if (isfinished == true)
                {
                    Console.WriteLine(entries.Value.id + ".) " + entries.Value.title);
                }

            }

           // ShowDetails(data, false, data.dict);
            Console.ReadKey();
            Console.Clear();
          
        }

        public Dictionary<int, Item> ShortEnddate(Data data, char sorter)
        {
            if (sorter == '>')
            {
                data.dict = data.dict.Where(x => (x.Value.enddate.Date > DateTime.Now.Date)).ToDictionary(x => x.Key, i => i.Value);
            }

            if (sorter == '<')
            {
                data.dict = data.dict.Where(x => (x.Value.enddate.Date < DateTime.Now.Date)).ToDictionary(x => x.Key, i => i.Value);
            }

            if (sorter == '=')
            {
                data.dict = data.dict.Where(x => (x.Value.enddate.Date == DateTime.Now.Date)).ToDictionary(x => x.Key, i => i.Value);
            }
            data.dict = data.dict.OrderBy(x => (x.Value.enddate.Date)).ToDictionary(x => x.Key, i => i.Value);
            return data.dict;
        }
    }


}
