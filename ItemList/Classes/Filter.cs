using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList.Classes
{
    class Filter
    {
        public void FilterMenu(Data data)
        {
            Console.WriteLine("Filter attributes");
            Console.WriteLine();
            Console.WriteLine("Show entries which are finished [f]");
            Console.WriteLine();
            Console.WriteLine("Attribute: Enddate");
            Console.WriteLine("Generalsort [e]");
            Console.WriteLine("enddate < today [e<]");
            Console.WriteLine("enddate > today [e>]");
            Console.WriteLine("enddate equals today [e=]");
            Console.WriteLine();
            Console.WriteLine("Attribute: Priority");
            Console.WriteLine("Generalsort [p]");
            Console.WriteLine("priority equals 'A' [pa]");
            Console.WriteLine("priority equals 'B' [pb]");
            Console.WriteLine("priority equals 'C' [pc]");
            string inputvalue = Console.ReadLine();

            if (inputvalue == "")
            {
                Console.Clear();
                return;
            }
            Console.Clear();
            shortDict(data, inputvalue);
        }

        public void shortDict(Data data, string sortattribute)
        {
            bool endate = false;
            bool isfinished = false;
            bool priority = false;
            var array = sortattribute.ToCharArray();

            if (array[0] == 'e')
            {
                endate = true;
                if (sortattribute == "e")
                {
                    data.dict = SortEnddate(data, 'x');
                }

                else if (array[1] == '<')
                {
                    data.dict = SortEnddate(data, '<');
                }
                else if (array[1] == '>')
                {
                    data.dict = SortEnddate(data, '>');
                }
                else if (array[1] == '=')
                {
                    data.dict = SortEnddate(data, '=');
                }
            }

            else if (array[0] == 'f')
            {
                isfinished = true;
                data.dict = EntriesSetFinished(data);
            }

            else if (array[0] == 'p')
            {
                priority = true;

                if (sortattribute == "p")
                {
                    data.dict = SortPriority(data, 'x');
                    DictContainsValues(data, data.dict);
                }

                else if (array[1] == 'a')
                {
                    data.dict = SortPriority(data, 'a');
                    DictContainsValues(data, data.dict);

                }
                else if (array[1] == 'b')
                {
                    data.dict = SortPriority(data, 'b');
                    DictContainsValues(data, data.dict);
                }
                else if (array[1] == 'c')
                {
                    data.dict = SortPriority(data, 'c');
                    DictContainsValues(data, data.dict);
                }
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

                if (priority == true)
                {
                    Console.WriteLine(entries.Value.id + ".) " + entries.Value.title + ": --> " + entries.Value.priority);
                }

            }

            // ShowDetails(data, false, data.dict);
            Console.ReadKey();
            Console.Clear();

        }

        public Dictionary<int, Item> EntriesSetFinished(Data data)
        {
            data.dict = data.dict.Where(x => (x.Value.isfinished == true)).ToDictionary(x => x.Key, i => i.Value);
            return data.dict;
        }

        public Dictionary<int, Item> SortEnddate(Data data, char sorter)
        {
            if (sorter == '>')
            {
                data.dict = data.dict.Where(x => (x.Value.enddate.Date > DateTime.Now.Date)).ToDictionary(x => x.Key, i => i.Value);
                DictContainsValues(data, data.dict);
            }

            if (sorter == '<')
            {
                data.dict = data.dict.Where(x => (x.Value.enddate.Date < DateTime.Now.Date)).ToDictionary(x => x.Key, i => i.Value);
                DictContainsValues(data, data.dict);
            }

            if (sorter == '=')
            {
                data.dict = data.dict.Where(x => (x.Value.enddate.Date == DateTime.Now.Date)).ToDictionary(x => x.Key, i => i.Value);
                DictContainsValues(data, data.dict);
            }
            data.dict = data.dict.OrderBy(x => (x.Value.enddate.Date)).ToDictionary(x => x.Key, i => i.Value);

            return data.dict;
        }

        public Dictionary<int, Item> SortPriority(Data data, char sorter)
        {
            if (sorter == 'a')
            {
                data.dict = data.dict.Where(x => (x.Value.priority == 'a')).ToDictionary(x => x.Key, i => i.Value);
            }

            if (sorter == 'b')
            {
                data.dict = data.dict.Where(x => (x.Value.priority == 'b')).ToDictionary(x => x.Key, i => i.Value);
            }

            if (sorter == 'c')
            {
                data.dict = data.dict.Where(x => (x.Value.priority == 'c')).ToDictionary(x => x.Key, i => i.Value);

            }
            data.dict = data.dict.Where(x => (x.Value.priority == 'a' || x.Value.priority == 'b' || x.Value.priority == 'c')).ToDictionary(x => x.Key, i => i.Value);
            data.dict = data.dict.OrderBy(x => (x.Value.priority)).ToDictionary(x => x.Key, i => i.Value);
            return data.dict;
        }

        public void DictContainsValues(Data data, Dictionary<int, Item> dict)
        {
            if (data.dict.Count == 0)
            {
                Console.WriteLine("No values avaible");
            }
        }
    }
}

