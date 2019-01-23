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
        // General class variables
        //string path = "data.txt";
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

        public Item( string title)
        {
            //this.id = id;
            this.title = title;
        }

        public Item()
        {
            
        }

        List<Item> itemlist = new List<Item>();
        Dictionary<int, Item> dict = new Dictionary<int, Item>();

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
            itemlist.Add(item);
        }

        public void DisplayAllItems()
        {
            foreach (Item entries in itemlist)
            {
                Console.WriteLine("Title " + entries.title);
                Console.WriteLine("Description " + entries.description);
               
            }
        }
        
    }
}

