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
    public class Data
    {
        

        // General class variables
        public string path = "data.txt";
        public string pathId = "idfile.txt";
        public string currentidStr = "1";
        public int currentid = 1;

        public Data()
        {

        }
        public Dictionary<int, Item> dict = new Dictionary<int, Item>();

        public void FileExists()
        {
            if (File.Exists(path))
            {
                dict = LoadList(path);


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

        public void SaveList(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            IFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, dict);

            fs.Close();
        }

        public Dictionary<int, Item> LoadList(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            dict = (Dictionary<int, Item>)bf.Deserialize(fs);
            fs.Close();

            return dict;
        }

        public void DeleteItem(int id, Item item)
        {
            dict.Remove(id);
            SaveList(path);
        }

        public void UpdateItem(int index, Item item)
        {
            Console.WriteLine("Update entry: ");
            Console.WriteLine();
            Console.WriteLine("Update Title");
            var inputtitle = Console.ReadLine();
            if (inputtitle != "")
            {
                item.title = inputtitle;
            }

            Console.WriteLine("Update description");
            var inputdescription = Console.ReadLine();
            if (inputdescription != "")
            {
                item.description = inputdescription;
            }

            SaveList(path);
            //Console.WriteLine("entry: " + item.id + ".)");
        }

        public int CountItems()
        {
            var itemscount = dict.Count();
            Console.WriteLine("Number of items " + itemscount);
            return itemscount;
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

        public void AddItem()
        {
            currentid = ReadId();
            currentid = CountId(currentid);
            SaveId(pathId, currentid);

            Console.WriteLine("Add Entry");
            Console.WriteLine("Enter Title:");
            string userTitle = Console.ReadLine();
            while (userTitle == "")
            {
                Console.WriteLine("Title must have a value");
                userTitle = Console.ReadLine();

            }
            Console.WriteLine();
            Console.WriteLine("Enter Description:");
            string userDescription = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Enter Enddate:");
            var userEnddate = DefaultFunctions.SetDateValue();

            Console.WriteLine();
            Item item = new Item(userTitle, currentid);
            item.description = userDescription;
            item.enddate = userEnddate;
            item.createdate = DateTime.Now;

            Console.WriteLine("Save this entry [y/n]");
            string inputuser = Console.ReadLine();

            if (inputuser == "n")
            {
                currentid--;
                SaveId(pathId, currentid);
                Console.Clear();
                return;
            }
            else
            {
                Console.Clear();
                dict.Add(item.id, item);
                SaveList(path);
            }

        }

        public void ClearList()
        {
            dict.Clear();
            currentid = 0;
            SaveId(pathId, currentid);
            SaveList(path);
            Console.WriteLine("List has been reseted");
        }

    }
}
