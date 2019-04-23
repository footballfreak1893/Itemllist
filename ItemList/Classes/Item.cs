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
        string currentidStr = "1";
        int currentid = 1;

        //Attributes
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isobsolete { get; set; }
        public bool isfinished { get; set; }
        //string category { get; set; }
        public char priority { get; set; }
        public DateTime createdate { get; set; }
        public DateTime enddate { get; set; }


        public Item(string title, int id)
        {
            this.id = id;
            this.title = title;
        }

        public Item()
        {

        }

    }
  

   
}

