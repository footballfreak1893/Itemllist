using System;

namespace ItemList
{
    [Serializable]
    public class Item
    {
        //Attributes
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isobsolete { get; set; }
        public bool isfinished { get; set; }
        string category { get; set; }
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

