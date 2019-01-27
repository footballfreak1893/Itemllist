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
    public class Item
    {
        // General class variables
        string path = "data.txt";
        string pathId = "idfile.txt";
        string pathDict = "dictfile.txt";
        string currentidStr = "1";
        int currentid = 1;



        //Attributes
        public int id { get; set; }
        public string title { get; set; }
        string description { get; set; }
        //string category { get; set; }
        //string priority { get; set; }
        //DateTime createdate { get; set; }
        //DateTime enddate { get; set; }

        List<Item> itemlist = new List<Item>();
        Dictionary<int, Item> dict = new Dictionary<int, Item>();

        public Item(string title, int id)
        {
            this.id = id;
            this.title = title;
        }

        public Item()
        {

        }

        public void FileExists()
        {
            if (File.Exists(path))
            {
                itemlist = LoadList(path);
                dict = DeserializeDict(pathDict);

            }
            if (File.Exists(pathId))
            {
                currentid = ReadId();
            }

            else
            {
                currentid = 0;
                SaveId(pathId, currentid);
            }

        }



        public void AddItem()
        {
            currentid = ReadId();
             currentid = CountId(currentid);
            SaveId(pathId, currentid);

            Console.WriteLine("Add Entry");
            Console.WriteLine("Enter Title:");
            string userTitle = Console.ReadLine();

            Console.WriteLine("Add Entry");
            Console.WriteLine("Enter Description:");
            string userDescription = Console.ReadLine();

            Item item = new Item(userTitle, currentid);
            item.description = userDescription;

            Console.WriteLine("Save this entry [y/n]");
            string inputuser = Console.ReadLine();

            if (inputuser == "n")
            {
                currentid--;
                SaveId(pathId, currentid);
                return;
            }
            else
            {
                itemlist.Add(item);
                dict.Add(item.id, item);
            }

        }

        public void DisplayAllItems()
        {
            foreach (Item entries in itemlist)
            {
                Console.WriteLine(entries.id+".) " +"Title " + entries.title);
                
                //Console.WriteLine("Title " + entries.title);
                //Console.WriteLine("Description " + entries.description);

            }
        }
      

        public void ShowDetails()
        {
            DisplayAllItems();
            Console.WriteLine("Enter ID to display details [number]");
            string inputid = Console.ReadLine();
            int id = Convert.ToInt16(inputid);
            Item item = dict[id];
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Titel: " + item.title);
            Console.WriteLine("Beschreibung: " + item.description);

            //int index = Convert.ToInt16(inputid);
            //index = index - 1;
            //Console.WriteLine(itemlist.ElementAt(index).id);
            //Console.WriteLine(itemlist.ElementAt(index).title);
            //Console.WriteLine(itemlist.ElementAt(index).description);


            Console.WriteLine("Update Item [u]");
            Console.WriteLine("Delete Item [d]");
            string inputvalue = Console.ReadLine();

            if(inputvalue == "u")
            {
                UpdateItem(id);
            }

            else if (inputvalue == "d")
            {
                DeleteItem(id,item);
            }
            else
            {
                return;
            }

        }

        public void DeleteItem(int id, Item item)
        {
            itemlist.RemoveAt(id-1);
           // Console.WriteLine(isdeleted);
            dict.Remove(id);
            //foreach(Item items in itemlist)
            //{
            //    items.id = items.id --;
            //}
            //SaveId(pathId, currentid - 1);
            // currentid = ReadId();
            //SaveList(path);
            // itemlist = LoadList(path);


            //Elemente ausblenden, Attribut erstellen in fe überprüfen mit Attribut ISObsolete
        }

        public void UpdateItem()
        {
            Console.WriteLine("Update Title");
            itemlist[0].title = Console.ReadLine();
            Console.WriteLine(itemlist.ElementAt(1).title);


        }

        public void UpdateItem(int index)
        {
            Console.WriteLine("Update Title");
            itemlist.ElementAt(index).title = Console.ReadLine();
            


        }
        public int CountItems()
        {
            var itemscount = itemlist.Count();
            Console.WriteLine("Number of items " + itemscount);
            return itemscount;
        }


        public void Exit()
        {
            SaveList(path);
            SaveId(pathId, currentid);
            SerializeDict(pathDict);
            Environment.Exit(1);
            
        }

        public void SaveList(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            IFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, itemlist);

            fs.Close();
        }

        public List<Item> LoadList(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            itemlist = (List<Item>)bf.Deserialize(fs);
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

        public Dictionary<int, Item> DeserializeDict(string pathDict)
        {
            System.IO.FileStream fs = new System.IO.FileStream(pathDict, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            dict = (Dictionary<int, Item>)bf.Deserialize(fs);
            fs.Close();

            return dict;
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

        public int CountId(int currentid)
        {
            currentid = currentid + 1;
            return currentid;
        }
    }
}

