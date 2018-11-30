using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ItemList
{
    [Serializable]
    class Item
    {
        string path = "data.txt";
        string pathId = "idfile.txt";
        string currentidStr = "1";
        int currentid = 1;


        List<Item> itemlist = new List<Item>();

        Dictionary<int, Item> dict = new Dictionary <int, Item>();
      

        //Attributes
        public int id { get; set; }
        public string title { get; set; }
        string description { get; set; }
        char category { get; set; }
        string priority { get; set; }
        DateTime createdate;
        DateTime enddate;

        public void FileExists()
        {

            if (File.Exists(path))
            {
                Deserialize(path);
            }
            else
            {
                Item item = new Item();

                item.id = currentid;
                currentid = item.id;
                currentid = CountId(currentid);
                SaveId(pathId, currentid);

                item.title = "defaultTitle";
                item.description = "defaultDescription";
                item.category = 'd';
                item.priority = "defaultPriority";
                itemlist.Add(item);
                dict.Add(item.id, item);
            }
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("List");
                Console.WriteLine();
                Console.WriteLine("Enter a menu");
                Console.WriteLine();
                Console.WriteLine("Create Item");
                Console.WriteLine("Enter 1");
                Console.WriteLine();
                Console.WriteLine("Serialize");
                Console.WriteLine("Enter 2");
                Console.WriteLine();
                Console.WriteLine("Display List");
                Console.WriteLine("Enter 3");
                Console.WriteLine();
                Console.WriteLine("Display overview of entries");
                Console.WriteLine("Enter 4");
                Console.WriteLine();
                Console.WriteLine("Display dict");
                Console.WriteLine("Enter 5");
                Console.WriteLine();

                string inputmenu = Console.ReadLine();

                if (inputmenu == "1")
                {
                    Create();
                    Console.Clear();
                }

                else if (inputmenu == "2")
                {
                    itemlist = Serialize(path);
                    Deserialize(path, itemlist);
                    Console.Clear();
                }

                else if (inputmenu == "3")
                {
                    displayList();
                    Console.Clear();
                }

                else if (inputmenu == "4")
                {
                    DisplayEntries(itemlist);
                    Console.Clear();
                }

                else if (inputmenu == "5")
                {
                    DisplayDict(dict);
                    Console.Clear();
                }

                else
                {
                    continue;
                }
            }
        }

        public List<Item> Serialize(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            IFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, itemlist);

            fs.Close();

            return itemlist;
        }

        public void displayList()
        {
            foreach (Item i in itemlist)
            {
                Console.WriteLine("ID " + i.id);
                Console.WriteLine("Titel " + i.title);
                Console.WriteLine("Beschreibuung " + i.description);
                Console.WriteLine("Kategorie " + i.category);
                Console.WriteLine("Priorität " + i.priority);
                Console.WriteLine();
                Console.ReadKey();
            }
        }


        public List<Item> Deserialize(string path, List<Item> itemlist)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            itemlist = (List<Item>)bf.Deserialize(fs);
            fs.Close();

            return itemlist;
        }


        public List<Item> Deserialize(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            itemlist = (List<Item>)bf.Deserialize(fs);
            fs.Close();

            return itemlist;
        }

        public void Create()
        {
            Item item = new Item();

            item.id = ReadId();

          int currentid =  CountId(item.id);

            SaveId(pathId, currentid);

            Console.WriteLine("Enter tile");
            item.title = Console.ReadLine();

            Console.WriteLine("Enter description");
            item.description = Console.ReadLine();

            Console.WriteLine("Enter category");
            item.category = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("Enter priority");
            item.priority = Console.ReadLine();

            item.createdate = DateTime.Now;

            itemlist.Add(item);
            dict.Add(item.id, item);

        }

        public int CountId(int currentid)
        {
            currentid = currentid+1;
            Console.WriteLine("C" +currentid);
            Console.ReadKey();
            return currentid;
        }

        public int ReadId()
        {
           currentidStr = File.ReadAllText(pathId);
           int  currentid = Convert.ToInt32(currentidStr);
            return currentid;
        }

        public void SaveId(string pathId, int currentid)
        {
            currentidStr = Convert.ToString(currentid);
            File.WriteAllText(pathId, currentidStr);
        }

        public void DisplayEntries(List<Item> itemlist)
        {
            foreach(Item i in itemlist)
            {

                Console.WriteLine(i.id + ".) " + i.title);
                
            }
            Console.WriteLine("Enter ID to display full entry");
            string testid = Console.ReadLine();

            int inputid = Convert.ToInt32(testid);
            Details(inputid);
        }

        public void Details(int inputid)
        {
            Item item = dict[inputid];
            Console.WriteLine("ID " + item.id);
            Console.WriteLine("Titel " + item.title);
            Console.WriteLine("Beschreibuung " + item.description);
            Console.WriteLine("Kategorie " + item.category);
            Console.WriteLine("Priorität " + item.priority);
            Console.WriteLine();
            Console.ReadKey();
        }

        public void DisplayDict(Dictionary<int, Item> dict)
        {
            foreach(KeyValuePair<int, Item> pair in dict)
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
        }
    }
}

