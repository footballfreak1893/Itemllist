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
        //string pathId = "idfile.txt";
        //string pathDict = "dictfile.txt";
        //string currentidStr = "1";
        //int currentid = 1;



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

        public Item(string title)
        {
            //this.id = id;
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

            }
            else
            {
                //FirstMenu
            }

        }



        public void AddItem()
        {
            Console.WriteLine("Add Entry");
            Console.WriteLine("Enter Title:");
            string userTitle = Console.ReadLine();

            Console.WriteLine("Add Entry");
            Console.WriteLine("Enter Description:");
            string userDescription = Console.ReadLine();

            Item item = new Item(userTitle);
            item.description = userDescription;

            Console.WriteLine("Save this entry [y/n]");
            string inputuser = Console.ReadLine();

            if (inputuser == "n")
            {
                return;
            }
            else
            {
                itemlist.Add(item);
            }

        }

        public void DisplayAllItems()
        {
            foreach (Item entries in itemlist)
            {
                Console.WriteLine("Title " + entries.title);
                Console.WriteLine("Description " + entries.description);

            }
        }

        public void DeleteItem()
        {
            itemlist.Remove(itemlist[0]);
        }

        public void UpdateItem()
        {
            Console.WriteLine("Update Title");
            itemlist[0].title =Console.ReadLine();
        }
        public void Exit()
        {
            SaveList(path);
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
    }
}

