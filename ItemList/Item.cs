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
        string pathDict = "dictfile.txt";
        string currentidStr = "1";
        int currentid = 1;

        List<Item> itemlist = new List<Item>();

        Dictionary<int, Item> dict = new Dictionary<int, Item>();

        //Attributes
        public int id { get; set; }
        public string title { get; set; }
        string description { get; set; }
        string category { get; set; }
        string priority { get; set; }
        DateTime createdate { get; set; }
        DateTime enddate { get; set; }


        //File Existiert
        public void FileExists()
        {
            if (File.Exists(path))
            {
                DeserializeItem(path);
                DeserializeDict(pathDict);
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
                item.category = "d";
                item.priority = "defaultPriority";
                item.createdate = DateTime.Now;
                itemlist.Add(item);
                dict.Add(item.id, item);

                itemlist = SerializeItem(path);
                DeserializeItem(path, itemlist);
                SerializeDict(pathDict);
                Console.Clear();
            }
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("<--->List<--->");
                Console.WriteLine();
                Console.WriteLine("Enter a menu");
                Console.WriteLine();
                Console.WriteLine("Create Item");
                Console.WriteLine("Enter 1");
                Console.WriteLine();
                Console.WriteLine("Display overview of entries");
                Console.WriteLine("Enter 2");
                Console.WriteLine();

                string inputmenu = Console.ReadLine();

                if (inputmenu == "1")
                {
                    Create();
                    itemlist = SerializeItem(path);
                    DeserializeItem(path, itemlist);
                    SerializeDict(pathDict);
                    Console.Clear();
                }
                
                else if (inputmenu == "2")
                {
                    DisplayEntries(itemlist);
                    Console.Clear();
                }

                else
                {
                    Console.Clear();
                    continue;
                }
            }
        }

        public List<Item> SerializeItem(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            IFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, itemlist);

            fs.Close();

            return itemlist;
        }

        public Dictionary<int, Item> SerializeDict(string pathDict)
        {
            System.IO.FileStream fs = new System.IO.FileStream(pathDict, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            IFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, dict);

            fs.Close();

            return dict;
        }

        public List<Item> DeserializeItem(string path, List<Item> itemlist)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            itemlist = (List<Item>)bf.Deserialize(fs);
            fs.Close();

            return itemlist;
        }

        public List<Item> DeserializeItem(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            itemlist = (List<Item>)bf.Deserialize(fs);
            fs.Close();

            return itemlist;
        }

        public Dictionary<int, Item> DeserializeDict(string pathDict)
        {
            System.IO.FileStream fs = new System.IO.FileStream(pathDict, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            dict = (Dictionary<int, Item>)bf.Deserialize(fs);
            fs.Close();

            return dict;
        }

        //public void displayList()
        //{
        //    foreach (Item i in itemlist)
        //    {
        //        Console.WriteLine("ID: " + i.id);
        //        Console.WriteLine("Titel: " + i.title);
        //        Console.WriteLine("Beschreibung: " + i.description);
        //        Console.WriteLine("Kategorie: " + i.category);
        //        Console.WriteLine("Priorität: " + i.priority);
        //        Console.WriteLine();
        //        Console.ReadKey();
        //    }
        //}

        public void Create()
        {
            Item item = new Item();

            item.id = ReadId();

            int currentid = CountId(item.id);

            SaveId(pathId, currentid);

            Console.WriteLine("Enter title");
            item.title = Console.ReadLine();
            if (item.title == "")
            {
                item.title = NotDefind(); 
            }

            Console.WriteLine("Enter description");
            item.description = Console.ReadLine();
            if (item.description == "")
            {
                item.description = NotDefind();
            }

            item.category = SetCategory();
            if (item.category == "")
            {
                item.category = NotDefind();
            }

            item.priority = SetPriority();
            if (item.priority == "")
            {
                item.priority = NotDefind();
            }

            Console.WriteLine("Enter endddate");
            item.enddate = SetDateValue();

            item.createdate = DateTime.Now;

            itemlist.Add(item);
            dict.Add(item.id, item);
        }

        

        public Item Update(Item item)
        {
            Console.WriteLine("Update tile");
            string ptitle = Console.ReadLine();
            if (ptitle != "")
            {
                item.title = ptitle;
            }

            Console.WriteLine("Update description");
            string pdescription = Console.ReadLine();
            if (pdescription != "")
            {
                item.description = pdescription;
            }
            //Console.WriteLine("Update category");
            //char pcategory = Convert.ToChar(Console.ReadLine());
            //if (pcategory != 'a')
            //{
            //    item.category = pcategory;
            //}

            Console.WriteLine("Update priority");
            string ppriority = Console.ReadLine();
            if (ppriority != "")
            {
                item.priority = ppriority;
            }
            return item;
        }

        //public void Delete(Item item)
        //{
        //    itemlist = SerializeItem(path);
        //    DeserializeItem(path, itemlist);
        //    SerializeDict(pathDict);
        //    displayList();
        //}

        public int CountId(int currentid)
        {
            currentid = currentid + 1;
            return currentid;
        }

        public int ReadId()
        {
            currentidStr = File.ReadAllText(pathId);
            int currentid = Convert.ToInt32(currentidStr);
            return currentid;
        }

        public void SaveId(string pathId, int currentid)
        {
            currentidStr = Convert.ToString(currentid);
            File.WriteAllText(pathId, currentidStr);
        }


        public void DisplayEntries(List<Item> itemlist)
        {
            foreach (Item i in itemlist)
            {

                Console.WriteLine(i.id + ".) " + i.title);

            }
            Console.WriteLine("Enter ID to display full entry");
            string inputidStr = Console.ReadLine();

            int inputid = Convert.ToInt32(inputidStr);
            Console.Clear();
            Details(inputid);
        }

        public void Details(int inputid)
        {
            Item item = dict[inputid];
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Titel: " + item.title);
            Console.WriteLine("Beschreibung: " + item.description);
            Console.WriteLine("Kategorie: " + item.category);
            Console.WriteLine("Priorität: " + item.priority);
            Console.WriteLine("Erstellt am: " + item.createdate);
            Console.WriteLine("Fällig am: " + item.enddate);
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("Update item:");
            Console.WriteLine("Enter 1");
            Console.WriteLine();
            Console.WriteLine("Delete item:");
            Console.WriteLine("Enter 2");
            Console.WriteLine();
            string inputvalue = Console.ReadLine();

            if (inputvalue == "1")
            {
                item = Update(item);
               
                itemlist.Remove(item);
                itemlist.Add(item);
                item = dict[inputid];
                SerializeItem(path);
                DeserializeItem(path);
                SerializeDict(pathDict);
                DeserializeDict(pathDict);
            }
            //else if (inputvalue == "2")
            //{
            //    item.Delete(item);
            //}
        }

        public DateTime SetDateValue()
        {
            Console.WriteLine("Enter Day");
            var pday = Console.ReadLine();
            int day = Convert.ToInt32(pday);

            Console.WriteLine("Enter Month");
            var pmonth = Console.ReadLine();
            int month = Convert.ToInt32(pmonth);

            Console.WriteLine("Enter Year");
            var pyear = Console.ReadLine();
            int year = Convert.ToInt32(pyear);

            var date = new DateTime(year, month, day);
            return date;
        }

        public string SetCategory()
        {
            Console.WriteLine("Enter Category");
            Console.WriteLine("Enter 1: --> Business");
            Console.WriteLine("Enter 2: --> Private");
            Console.WriteLine("Enter 3: --> Future plan");
            var inputvalue = Console.ReadLine();
            string category ="";

            if (inputvalue == "1")
            {
                category = "Business";
            }
            else if (inputvalue == "2")
            {
                category = "Private";
            }
            else if (inputvalue == "3")
            {
                category = "Future Plan";
            }
            return category;
        }

        public string SetPriority()
        {
            Console.WriteLine("Enter Priority");
            Console.WriteLine("Enter 1: --> high");
            Console.WriteLine("Enter 2: --> medium");
            Console.WriteLine("Enter 3: --> low");
            var inputvalue = Console.ReadLine();
            string priority = "";

            if (inputvalue == "1")
            {
                priority = "high";
            }
            else if (inputvalue == "2")
            {
                priority = "medium";
            }
            else if (inputvalue == "3")
            {
                priority = "low";
            }
            return priority;
        }

        public string NotDefind()
        {
            string notDefind = "Not Defind";
            return notDefind;
        }

        public void DisplayDict(Dictionary<int, Item> dict)
        {
            foreach (KeyValuePair<int, Item> pair in dict)
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
        }
    }
}

